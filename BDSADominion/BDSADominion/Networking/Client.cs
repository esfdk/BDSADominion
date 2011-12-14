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
        /// The buffer of the Client
        /// </summary>
        private byte[] buffer = new byte[NetworkConst.BUFFERSIZE];

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="address">
        /// The IPAddress to connect to
        /// </param>
        internal Client(IPAddress address)
        {
            IPEndPoint end = new IPEndPoint(address, NetworkConst.PORT);

            Comm = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Comm.Connect(end);

            NewMessageEvent += RecievedMessage;

            BeginReceive();
        }

        /// <summary>
        /// This event tells when the Client receives a new message
        /// </summary>
        internal event ClientMessageHandler NewMessageEvent;

        /// <summary>
        /// Gets the Socket through which communication takes place.
        /// </summary>
        internal Socket Comm { get; private set; }

        /// <summary>
        /// Call this to make the client listen for new messages
        /// </summary>
        internal void BeginReceive()
        {
            Comm.BeginReceive(buffer, 0, NetworkConst.BUFFERSIZE, 0, BeginReceiveCallback, this);
        }

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
        }

        /// <summary>
        /// This is called when the client receives a message
        /// </summary>
        /// <param name="asyncResult">
        /// The async result.
        /// </param>
        private void BeginReceiveCallback(IAsyncResult asyncResult)
        {
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
