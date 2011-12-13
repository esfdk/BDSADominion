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
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk) based heavily on code from Simon Henriksen (shen@itu.dk)
    /// </author>
    internal class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        internal Client(IPAddress ipAddress)
        {
            IPEndPoint ipEnd = new IPEndPoint(ipAddress, NetworkConst.PORT);

            Comm = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Comm.Connect(ipEnd);

            NewMessageEvent += RecievedMessage;

            BeginReceive();

            ////Comm.BeginReceive(buffer, 0, buffer.length, 0, new AsyncCallback(AcceptReceive), Comm);
        }

        internal event ClientMessageHandler NewMessageEvent;

        /// <summary>
        /// Gets the Socket through which communication takes place.
        /// </summary>
        internal Socket Comm { get; private set; }

        private byte[] buffer = new byte[NetworkConst.BUFFERSIZE];

        ////internal StringBuilder stringBuilder = new StringBuilder();

        /// <summary>
        /// This method should be called whenever the client recieves a message from the server.
        /// </summary>
        /// <param name="message">
        /// The recieved message
        /// </param>
        private void RecievedMessage(string message)
        {
            string[] messageParts = message.Split(new char[] { '|' });
            Console.WriteLine("Client.RecievedMessage: Client received '{0}' of type {1} from player {2}", messageParts[2], messageParts[1], messageParts[0]);
            //stringBuilder.Clear();
            //BeginReceive();
        }

        public void BeginReceive()
        {
            ////Console.WriteLine("Client.BeginRecieve: Client BeginRecieve began");
            Comm.BeginReceive(buffer, 0, NetworkConst.BUFFERSIZE, 0, BeginReceiveCallback, this);
        }

        //TODO: Contract: may only be called if end is <EOF>
        private void BeginReceiveCallback(IAsyncResult asyncResult)
        {
            ////Console.WriteLine("Client.BeginRecieveCallback: Client recieve begun");
            int read = Comm.EndReceive(asyncResult);
            if (read > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(NetworkConst.ENCODER.GetString(buffer, 0, read));

                string content = stringBuilder.ToString();

                if (content.IndexOf("<EOF>") >= 0)
                {
                    string message = content.Substring(0, content.Length - 5);

                    if (NewMessageEvent != null)
                    {
                        NewMessageEvent(message);
                        Console.WriteLine("Client.BeginReceiveCallback: Begin Receive reached");
                    }

                    stringBuilder.Clear();
                }
                BeginReceive();
            }
        }
    }
}
