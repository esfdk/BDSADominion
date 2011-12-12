using System;

namespace BDSADominion.Networking
{
    using System.Linq;
    using System.Net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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
            SetNumberOfClients(2); //TODO hardcoded number of total clients
        }

        //May only be called if this is server //TODO contract
        public string GetServerIp()
        {
            return server.Ip.ToString();
        }

        private void SetNumberOfClients(int otherClients)
        {
            responseMessages = new string[otherClients];
            EmptyResponses();
        }

        private void EmptyResponses()
        {
            Console.WriteLine("Emptying messages");
            for (int i = 0; i < responseMessages.Length; i++)
            {
                responseMessages[i] = string.Empty;
            }
        }

        //May only be called with a string not containing '|', '<' and '>' //TODO Contract
        public string[] TurnMessage(string message)
        {
            EmptyResponses();
            string typeMessage = string.Format("{0}|{1}", MessageType.Action, message);
            client.Comm.Send(NetworkConst.ENCODER.GetBytes(typeMessage));
            while (responseMessages.Any(mes => mes.Equals(string.Empty)))
            {
                //Waiting for responses
            }
            return null;
        }

        private string ResponseMessage()
        {
            string typeMessage = string.Format("{0}|{1}", MessageType.Response, "<MR>");
            return typeMessage;
        }

        public event InterfaceMessageHandler MessageReceived;

        private void ReceivedNewMessage(string message)
        {
            string[] messageParts = message.Split(new char[] { '|' });
            //Console.WriteLine("Client received '{0}' from player {1}", messageParts[1], messageParts[0]);
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
                        responseMessages[int.Parse(messageParts[0]) - 1] = messageParts[2];
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
