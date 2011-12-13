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

        public NetworkingInterface() //If host
        {
            server = new Server();
            server.Start();
            AlwaysDo(server.Ip);
        }

        public NetworkingInterface(IPAddress ip) //If joining a game
        {
            AlwaysDo(ip);
        }

        private void AlwaysDo(IPAddress ip)
        {
            client = new Client(ip);
            client.NewMessageEvent += ReceivedNewMessage;
            ////SetNumberOfClients(2); //TODO hardcoded number of total clients. Must be updated with each serverMessage
        }

        //May only be called if this is server //TODO contract
        public string GetServerIp()
        {
            return server.Ip.ToString();
        }

        public void SetNumberOfClients(int totalClients)
        {
            responseMessages = new string[totalClients-1];
            EmptyResponses();
        }

        private void EmptyResponses()
        {
            Console.WriteLine("Emptying messages");
            for (int i = 0; i < responseMessages.Length; i++)
            {
                responseMessages[i] = string.Empty;
            }
            client.stringBuilder.Clear();
            client.BeginReceive();
        }

        //May only be called with a string not containing '|', '<' and '>' //TODO Contract
        public string[] TurnMessage(string message)
        {
            EmptyResponses();
            string typeMessage = string.Format("{0}|{1}<EOF>", MessageType.Action, message);
            client.Comm.Send(NetworkConst.ENCODER.GetBytes(typeMessage));
            while (responseMessages.Any(mes => mes.Equals(string.Empty)))
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
                        MessageReceived(messageParts[2], fromPlayer); // TODO ResponseWait, how to know?
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
                        //TODO ResponseWait Message
                        break;
                    default:
                    //TODO Error
                    break;
                }
            }

            ////MessageReceived
        }
    }
}
