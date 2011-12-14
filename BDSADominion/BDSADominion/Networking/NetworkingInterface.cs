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
        private Server server;
        private Client client;

        public bool IsServer { get; private set; }

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

        private void AlwaysDo(IPAddress ip)
        {
            client = new Client(ip);
            client.NewMessageEvent += ReceivedNewMessage;
            ////SetNumberOfClients(2); //TODO hardcoded number of total clients. Must be updated with each serverMessage
        }

        public string GetServerIp()
        {
            Contract.Requires(IsServer);

            return server.Ip.ToString();
        }

        public void SetNumberOfClients(int totalClients)
        {
            responseMessages = new string[totalClients-1];
            EmptyResponses();
        }

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

        public void PreGameMessage(string message)
        {
            string typeMessage = string.Format("{0}|{1}<EOF>", MessageType.System, message);
            client.Comm.Send(NetworkConst.ENCODER.GetBytes(typeMessage));
        }

        private string ResponseMessage()
        {
            string typeMessage = string.Format("{0}|{1}<EOF>", MessageType.Response, "<MR>");
            return typeMessage;
        }

        public event InterfaceMessageHandler MessageReceived;

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
