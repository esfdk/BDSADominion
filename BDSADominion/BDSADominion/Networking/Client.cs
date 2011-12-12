namespace BDSADominion.Networking
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// This class is a client that can be used to connect to a server,
    /// to facilitate communication over a network. The communication must be strings.
    /// </summary>
    internal class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IPAddress ipAddress)
        {
            

            IPEndPoint ipEnd = new IPEndPoint(ipAddress, NetworkConst.PORT);

            Comm = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Comm.Connect(ipEnd);

            BeginReceive();

            ////Comm.BeginReceive(buffer, 0, buffer.length, 0, new AsyncCallback(AcceptReceive), Comm);
        }

        public event ClientMessageHandler NewMessageEvent;

        /// <summary>
        /// Gets the Socket through which communication takes place.
        /// </summary>
        public Socket Comm { get; private set; }

        private byte[] buffer = new byte[NetworkConst.BUFFERSIZE];

        private StringBuilder stringBuilder = new StringBuilder();

        /// <summary>
        /// This method should be called whenever the client recieves a message from the server.
        /// </summary>
        /// <param name="senderId">
        /// The network Id of the player responisble for sending the message in the first place.
        /// </param>
        /// <param name="message">
        /// The recieved message
        /// </param>
        public void RecievedMessage(string message)
        {
            string[] messageParts = message.Split(new char[] { '|' });
            Console.WriteLine("Client received '{0}' from player {1}", messageParts[1], messageParts[0]);
        }

        public void BeginReceive()
        {
            Comm.BeginReceive(buffer, 0, NetworkConst.BUFFERSIZE, 0, BeginReceiveCallback, this);
        }

        private void BeginReceiveCallback(IAsyncResult asyncResult)
        {
            Console.WriteLine("Client recieve begun");
            int read = Comm.EndReceive(asyncResult);

            if (read > 0)
            {
                stringBuilder.Append(NetworkConst.ENCODER.GetString(buffer, 0, read));

                string content = stringBuilder.ToString();

                ////Console.WriteLine(content);

                if (content.IndexOf("<EOF>") >= 0)
                {
                    string message = content.Substring(0, content.Length - 5);

                    if (NewMessageEvent != null)
                    {
                        NewMessageEvent(message);
                    }

                    stringBuilder.Clear();
                }

                BeginReceive();
            }
        }
    }
}
