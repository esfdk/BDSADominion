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
        /// <summary>
        /// The Socket that handles communication
        /// </summary>
        private readonly Socket socket;

        /// <summary>
        /// The clients that are connected keyed by their id
        /// </summary>
        private readonly Dictionary<int, Connection> connectedClients = new Dictionary<int, Connection>();

        /// <summary>
        /// the current highest Id
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        internal Server()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ClientConnectedEvent += ClientConnected;
        }

        /// <summary>
        /// event tells when a client connects
        /// </summary>
        internal event ConnectedClientHandler ClientConnectedEvent;

        /// <summary>
        /// Gets Ip of this computer, that clients can connect to
        /// </summary>
        internal IPAddress Ip
        {
            get
            {
                return
                    Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(
                        ip => ip.ToString().Count(chr => chr.Equals('.')) == 3).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets an incrementing values, which is the next assignment of Id
        /// </summary>
        private int NextIdAssign
        {
            get
            {
                id += 1;
                return id;
            }
        }

        /// <summary>
        /// This starts the Server. Must be called before the Server is used
        /// </summary>
        internal void Start()
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, NetworkConst.PORT));
            socket.Listen(100);

            socket.BeginAccept(AcceptCallback, socket);
        }

        /// <summary>
        /// Returns a list of all clients connected to the server
        /// </summary>
        /// <returns>
        /// The list
        /// </returns>
        internal List<Connection> GetClientList()
        {
            return connectedClients.Values.ToList();
        }

        /// <summary>
        /// This sends a system message to all clients exept the specified
        /// </summary>
        /// <param name="message">
        /// The message to be sent
        /// </param>
        /// <param name="client">
        /// The client that originally sent the message, who it will not be sent back to
        /// </param>
        internal void SystemMessage(string message, Connection client)
        {
            string compoundMessage = string.Format("{0}|{1}|{2}<EOF>", 0, MessageType.System, message);

            client.Send(compoundMessage);
        }

        /// <summary>
        /// Send a system message to all clients
        /// </summary>
        /// <param name="message">
        /// The message to be sent
        /// </param>
        internal void SystemMessage(string message)
        {
            GetClientList().ForEach(cli => SystemMessage(message, cli));
        }

        /// <summary>
        /// Use this to send a message recieved from one client to all clients except the sender.
        /// </summary>
        /// <param name="message">
        /// The message, must be clean
        /// </param>
        /// <param name="clientId">
        /// The Id of the sender.
        /// </param>
        /// <param name="type">
        /// The type of message being sent
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

        /// <summary>
        /// Called when a client connects to the server.
        /// </summary>
        /// <param name="asyncResult">
        /// The async result.
        /// </param>
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

        /// <summary>
        /// Called when a client disconnect
        /// </summary>
        /// <param name="connect">
        /// The Connection
        /// </param>
        private void ClientConnectionDisconnected(Connection connect)
        {
            connectedClients.Remove(connect.Id);
        }

        /// <summary>
        /// Called when the server receives a message
        /// </summary>
        /// <param name="conn">
        /// The connection
        /// </param>
        /// <param name="message">
        /// The message to be sent
        /// </param>
        private void ServerRecievedMessage(Connection conn, string message)
        {
            string[] messageParts = message.Split(new char[] { '|' });
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

        /// <summary>
        /// Called when a client connects
        /// </summary>
        /// <param name="conn">
        /// The Connection
        /// </param>
        /// <returns>
        /// If a client connected
        /// </returns>
        private bool ClientConnected(Connection conn)
        {
            Console.WriteLine("Server.ClientConnected: Client {0} connected", conn.Id);
            conn.ReceivedMessageEvent += ServerRecievedMessage;

            return true;
        }
    }
}
