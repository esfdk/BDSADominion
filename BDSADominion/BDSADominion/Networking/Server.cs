namespace BDSADominion.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// This is the server that handles all communication over the network
    /// </summary>
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk) based heavily on code from Simon Henriksen (shen@itu.dk)
    /// </author>
    internal class Server
    {
        ////private byte[] messageBuffer = new byte[NetworkConst.BUFFERSIZE];

        private Socket socket;

        private Dictionary<int, Connection> connectedClients = new Dictionary<int, Connection>();

        private int id = 0;

        private int NextIdAssign
        {
            get
            {
                id += 1;
                return id;
            }
        }

        internal Server()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ClientConnectedEvent += ClientConnected;
            //ClientConnectedEvent += ServerPreGameMessage;
        }

        /*private bool ServerPreGameMessage(Connection connection)
        {
            int currentNumberOfPlayers = GetClientList().Count;
            SystemMessage(string.Format("<YPN{0}>", connection.Id), connection);
            SystemMessage(string.Format("<CP{0}>", currentNumberOfPlayers));

            return true; // TODO What?
        }*/

        internal event ConnectedClientHandler ClientConnectedEvent;

        internal IPAddress Ip
        {
            get
            {
                return
                    Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(
                        ip => ip.ToString().Count(chr => chr.Equals('.')) == 3).FirstOrDefault();
            }
        }

        internal void Start()
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, NetworkConst.PORT));
            socket.Listen(100);

            socket.BeginAccept(AcceptCallback, socket);
        }

        internal void SystemMessage(string message, Connection client)
        {
            string compoundMessage = string.Format("{0}|{1}|{2}<EOF>", 0, MessageType.System, message);

            client.Send(compoundMessage);
        }

        internal void SystemMessage(string message)
        {
            GetClientList().ForEach(cli => SystemMessage(message, cli));
        }

        /// <summary>
        /// Use this to send a message recieved from one client to all clients except the sender.
        /// </summary>
        /// <param name="message">
        /// The clean message
        /// </param>
        /// <param name="clientId">
        /// The Id of the sender.
        /// </param>
        internal void ForwardMessage(string message, int clientId, MessageType type)
        {
            ////Console.WriteLine("Server.ForwardMessage: Forwarding message: " + message);
            string compoundMessage = string.Format("{0}|{1}|{2}<EOF>", clientId, type, message);

            foreach (Connection connection in GetClientList().Where(con => con.Id != clientId))
            {
                connection.Send(compoundMessage);
            }
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            Socket server = (Socket)asyncResult.AsyncState;
            Socket client = server.EndAccept(asyncResult);

            Connection conn = new Connection(client, NextIdAssign);

            server.BeginAccept(AcceptCallback, server);

            if (ClientConnectedEvent != null & !ClientConnectedEvent(conn))
            {
                client.Close();
                return;
            }

            connectedClients.Add(conn.Id, conn);

            conn.ClosedConnectionEvent += ClientConnectionDisconnected;

            conn.BeginReceive();
        }

        private void ClientConnectionDisconnected(Connection connect)
        {
            connectedClients.Remove(connect.Id);
        }

        private void ServerRecievedMessage(Connection conn, string message)
        {
            string[] messageParts = message.Split(new char[] {'|'});
            ////Console.WriteLine("Server.ServerReceivedMessage: Server received '{0}' of type {1} from player {2}", messageParts[1], messageParts[0], conn.Id);
            if (messageParts[1].StartsWith("<STGM>") && conn.Id == 1)
            {
                ////Console.WriteLine("Server.ServerReceivedMessage: Start Game!");
                foreach (KeyValuePair<int, Connection> connectedClient in connectedClients)
                {
                    SystemMessage(string.Format("<STGM>,{0},{1}", connectedClients.Count, connectedClient.Key), connectedClient.Value);
                }
            }
            else
            {
                MessageType messageType;
                MessageType.TryParse(messageParts[0], out messageType);
                ForwardMessage(messageParts[1], conn.Id, messageType);
            }
        }

        private bool ClientConnected(Connection conn)
        {
            Console.WriteLine("Server.ClientConnected: Client {0} connected", conn.Id);
            conn.ReceivedMessageEvent += ServerRecievedMessage;

            return true;
        }

        public List<Connection> GetClientList()
        {
            return connectedClients.Values.ToList();
        }
    }
}
