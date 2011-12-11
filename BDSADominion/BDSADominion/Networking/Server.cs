namespace BDSADominion.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    internal class Server
    {
        private byte[] messageBuffer = new byte[NetworkConst.BUFFERSIZE];

        private Socket socket;

        private Dictionary<int, Connection> connectedClients = new Dictionary<int, Connection>();

        ////public List<Connection> clientList = new List<Connection>();

        private int id = 0;

        private int NextIdAssign
        {
            get
            {
                id += 1;
                return id;
            }
        }

        public Server()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ClientConnectedEvent += ClientConnected;
        }

        public event ConnectedClientHandler ClientConnectedEvent;

        public IPAddress Ip
        {
            get
            {
                return
                    Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(
                        ip => ip.ToString().Count(chr => chr.Equals('.')) == 3).FirstOrDefault();
            }
        }

        public void Start()
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, NetworkConst.PORT));
            socket.Listen(100);

            socket.BeginAccept(AcceptCallback, socket);
        }

        public void SystemMessage(string message)
        {
            string compoundMessage = string.Format("{0}|{1}<EOF>", 0, message);

            GetClientList().ForEach(cli => cli.Send(compoundMessage));
        }

        /// <summary>
        /// Use this to send a message recieved from one client to all clients except the sender.
        /// </summary>
        /// <param name="message">
        /// The recieved message
        /// </param>
        /// <param name="clientId">
        /// The Id of the sender.
        /// </param>
        public void ForwardMessage(string message, int clientId)
        {
            Console.WriteLine("Forwarding message");
            string compoundMessage = string.Format("{0}|{1}<EOF>", clientId, message);

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

        public void ServerRecievedMessage(Connection conn, string message)
        {
            Console.WriteLine("Server received '{0}' from player {1}", message, conn.Id);
            ForwardMessage(message, conn.Id);
        }

        public bool ClientConnected(Connection conn)
        {
            Console.WriteLine("Client {0} connected", conn.Id);
            conn.ReceivedMessageEvent += ServerRecievedMessage;
            ////conn.BeginReceive();

            return true; //TODO
        }

        public List<Connection> GetClientList()
        {
            return connectedClients.Values.ToList();
        }
    }
}
