namespace BDSADominion.Networking
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// Connection is the servers way of holding a Client and everything related to him
    /// </summary>
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk) based heavily on code from Simon Henriksen (shen@itu.dk)
    /// </author>
    internal class Connection
    {
        /// <summary>
        /// The Socket through which the connection to the client is maintained
        /// </summary>
        private readonly Socket connSocket;

        /// <summary>
        /// the buffer for the Connection
        /// </summary>
        private byte[] buffer = new byte[NetworkConst.BUFFERSIZE];

        /// <summary>
        /// The Stringbuilder the connection uses to build the messages
        /// </summary>
        private StringBuilder stringBuilder = new StringBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="clientSocket">
        /// The client's socket.
        /// </param>
        /// <param name="id">
        /// The Id of the Client
        /// </param>
        internal Connection(Socket clientSocket, int id)
        {
            connSocket = clientSocket;
            Id = id;
        }

        /// <summary>
        /// Tells when a message is coming from the Client
        /// </summary>
        internal event ServerMessageHandler ReceivedMessageEvent;

        /// <summary>
        /// Tells when the client disconnects
        /// </summary>
        internal event ClosedConnectionHandler ClosedConnectionEvent;

        /// <summary>
        /// Gets the IP of the Client
        /// </summary>
        internal IPAddress ClientIp
        {
            get { return ((IPEndPoint)connSocket.LocalEndPoint).Address; }
        }

        /// <summary>
        /// Gets Id of the Client
        /// </summary>
        internal int Id { get; private set; }

        /// <summary>
        /// Sends the message to the Client
        /// </summary>
        /// <param name="message">
        /// The message, must be compound.
        /// </param>
        internal void Send(string message)
        {
            connSocket.Send(NetworkConst.ENCODER.GetBytes(message));
        }

        /// <summary>
        /// Call this to make the Connection listen for new messages
        /// </summary>
        internal void BeginReceive()
        {
            connSocket.BeginReceive(buffer, 0, NetworkConst.BUFFERSIZE, 0, BeginReceiveCallback, this);
        }

        /// <summary>
        /// This is called when a message is recieved from the Socket
        /// </summary>
        /// <param name="asyncResult">
        /// The async result.
        /// </param>
        private void BeginReceiveCallback(IAsyncResult asyncResult)
        {
            ////Console.WriteLine("Recieve begun on Connection");
            int read = connSocket.EndReceive(asyncResult);

            if (read > 0)
            {
                stringBuilder.Append(NetworkConst.ENCODER.GetString(buffer, 0, read));

                string content = stringBuilder.ToString();

                if (content.IndexOf("<EOF>") >= 0)
                {
                    string message = content.Substring(0, content.Length - 5);

                    if (ReceivedMessageEvent != null)
                    {
                        ReceivedMessageEvent(this, message);
                    }

                    stringBuilder.Clear();
                }

                BeginReceive();
            }
            else
            {
                ClosedConnectionEvent(this);
            }
        }
    }
}
