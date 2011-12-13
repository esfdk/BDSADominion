namespace BDSADominion.Networking
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// </summary>
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk) based heavily on code from Simon Henriksen (shen@itu.dk)
    /// </author>
    internal class Connection
    {
        private readonly Socket connSocket;

        private byte[] buffer = new byte[NetworkConst.BUFFERSIZE];

        private StringBuilder stringBuilder = new StringBuilder();

        internal IPAddress ClientIp
        {
            get { return ((IPEndPoint) connSocket.LocalEndPoint).Address; }
        }

        internal int Id { get; private set; }

        internal event ServerMessageHandler ReceivedMessageEvent;

        internal event ClosedConnectionHandler ClosedConnectionEvent;

        internal Connection(Socket clientSocket, int id)
        {
            connSocket = clientSocket;
            Id = id;
        }

        /// <summary>
        /// Sends the message to the Client
        /// </summary>
        /// <param name="message">
        /// The message, must be clean.
        /// </param>
        internal void Send(string message)
        {
            connSocket.Send(NetworkConst.ENCODER.GetBytes(message));
        }

        internal void BeginReceive()
        {
            connSocket.BeginReceive(buffer, 0, NetworkConst.BUFFERSIZE, 0, BeginReceiveCallback, this);
        }

        private void BeginReceiveCallback(IAsyncResult asyncResult)
        {
            ////Console.WriteLine("Recieve begun on Connection");
            int read = connSocket.EndReceive(asyncResult);

            if (read > 0)
            {
                stringBuilder.Append(NetworkConst.ENCODER.GetString(buffer, 0, read));

                string content = stringBuilder.ToString();

                Console.WriteLine(content);

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
