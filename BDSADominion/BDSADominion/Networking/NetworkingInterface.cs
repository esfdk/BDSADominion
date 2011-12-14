using System.Diagnostics.Contracts;
using System.Net.Sockets;

namespace BDSADominion.Networking
{
    using System;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk)
    /// </author>
    public class NetworkingInterface
    {
        /// <summary>
        /// The server (if used)
        /// </summary>
        private Server server;

        /// <summary>
        /// The client
        /// </summary>
        private Client client;

        /// <summary>
        /// Holds the ResponseMessages recieved from other player
        /// </summary>
        private string[] responseMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkingInterface"/> class.
        /// This should be called if the <see cref="NetworkingInterface"/> is to represent a server
        /// </summary>
        public NetworkingInterface()
        {
            server = new Server();
            server.Start();
            IsServer = true;
            AlwaysDo(server.Ip);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkingInterface"/> class.
        /// This should be called if the <see cref="NetworkingInterface"/> is to represent a client
        /// that is joining a host of a session
        /// </summary>
        /// <param name="ip">
        /// The IP if the server that the client should connect to.
        /// </param>
        public NetworkingInterface(IPAddress ip)
        {
            Contract.Ensures(ip != null);
            Contract.Ensures(ip.AddressFamily == AddressFamily.InterNetwork);
            IsServer = false;
            AlwaysDo(ip);
        }

        /// <summary>
        /// Tells when a new message is recieved
        /// </summary>
        public event InterfaceMessageHandler MessageReceived;

        /// <summary>
        /// Gets a value indicating whether this NetworkingInterface runs a Server.
        /// </summary>
        public bool IsServer { get; private set; }

        /// <summary>
        /// Gets the IP of the server, if this is running a Server
        /// </summary>
        /// <returns>
        /// the IP of the Server
        /// </returns>
        public string GetServerIp()
        {
            Contract.Requires(IsServer);

            return server.Ip.ToString();
        }

        /// <summary>
        /// This sets the Response List to a new value
        /// </summary>
        /// <param name="totalClients">
        /// The total number of people in the game
        /// </param>
        public void SetResponseList(int totalClients)
        {
            responseMessages = new string[totalClients - 1];
            EmptyResponses();
        }

        /// <summary>
        /// This is called when a message should be sent to the other clients
        /// </summary>
        /// <param name="message">
        /// The message to be sent
        /// </param>
        /// <returns>
        /// The list of Messages of responses
        /// </returns>
        public string[] TurnMessage(string message)
        {
            Contract.Requires(!message.Contains("|"));
            Contract.Requires(!message.Contains("<"));
            Contract.Requires(!message.Contains(">"));

            EmptyResponses();
            string typeMessage = string.Format("{0}|{1}<EOF>", MessageType.Action, message);
            client.Comm.Send(NetworkConst.ENCODER.GetBytes(typeMessage));
            ////while (responseMessages.Any(mes => mes.Equals(string.Empty)))
            {

            }
            string[] responses = new string[responseMessages.Length];
            responseMessages.CopyTo(responses, 0);
            return responses;
        }

        /// <summary>
        /// this should be called for pregame messages
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void PreGameMessage(string message)
        {
            string typeMessage = string.Format("{0}|{1}<EOF>", MessageType.System, message);
            client.Comm.Send(NetworkConst.ENCODER.GetBytes(typeMessage));
        }

        /// <summary>
        /// This should always be done by the constructor
        /// </summary>
        /// <param name="ip">
        /// The IP if the server
        /// </param>
        private void AlwaysDo(IPAddress ip)
        {
            client = new Client(ip);
            client.NewMessageEvent += ReceivedNewMessage;
        }

        /// <summary>
        /// Empty the ResponseList
        /// </summary>
        private void EmptyResponses()
        {
            ////Console.WriteLine("Emptying messages");
            for (int i = 0; i < responseMessages.Length; i++)
            {
                responseMessages[i] = string.Empty;
            }
            ////client.stringBuilder.Clear();
            client.BeginReceive();
        }

        /// <summary>
        /// This creates the response message
        /// </summary>
        /// <returns>
        /// The message of Response
        /// </returns>
        private string ResponseMessage()
        {
            string typeMessage = string.Format("{0}|{1}<EOF>", MessageType.Response, "<MR>");
            return typeMessage;
        }

        /// <summary>
        /// This is called whenever a message comes in through the Client
        /// </summary>
        /// <param name="message">
        /// The message received
        /// </param>
        private void ReceivedNewMessage(string message)
        {
            string[] messageParts = message.Split(new char[] { '|' });
            ////Console.WriteLine("NetworkingInterface.ReceivedNewMessage: Client received '{0}' from player {1}", messageParts[2], messageParts[0]);
            int fromPlayer;
            MessageType type;
            bool playerParse = int.TryParse(messageParts[0], out fromPlayer);
            bool messageParse = MessageType.TryParse(messageParts[1], out type);
            if (playerParse & messageParse)
            {
                switch (type)
                {
                    case MessageType.System:
                        MessageReceived(messageParts[2], fromPlayer);
                        break;
                    case MessageType.Action:
                        MessageReceived(messageParts[2], fromPlayer);
                        client.Comm.Send(NetworkConst.ENCODER.GetBytes(ResponseMessage()));
                        break;
                    case MessageType.Response:
                        int playerId = int.Parse(messageParts[0]) - 1;
                        if (playerId < responseMessages.Length)
                        {
                            responseMessages[playerId] = messageParts[2];
                        }
                        
                        break;
                    case MessageType.WaitResponse:
                        //MessageWait is not implemented and not needed by any cards.
                        break;
                    default:
                        Console.WriteLine("NetworkInterface.ReceivedNewMessage: Unrecognized MessageType");
                        break;
                }
            }

            ////MessageReceived
        }
    }
}
