namespace BDSADominion.Networking
{
    using System.Text;

    /// <summary>
    /// This is the delegate used when a client connects to the server
    /// </summary>
    /// <param name="connection">
    /// The Connection through which the client is connected to the server
    /// </param>
    /// <returns>
    /// Whether a client connected correctly //TODO
    /// </returns>
    public delegate bool ConnectedClientHandler(Connection connection);

    /// <summary>
    /// This is the delegate used when a Connection is closed.
    /// </summary>
    /// <param name="connection">
    /// The Connection through which the client was connected to the server
    /// </param>
    public delegate void ClosedConnectionHandler(Connection connection);

    /// <summary>
    /// This is the delegate used when the server receives a message
    /// </summary>
    /// <param name="connection">
    /// The Connection through which the client is connected to the server
    /// </param>
    /// <param name="message">
    /// The message that has been received
    /// </param>
    public delegate void ServerMessageHandler(Connection connection, string message);

    /// <summary>
    /// This is the delegate used when a client recieves a message
    /// </summary>
    /// <param name="message">
    /// The message that has been recieved
    /// </param>
    public delegate void ClientMessageHandler(string message);

    public delegate void InterfaceMessageHandler(string message, int sender);

    /// <summary>
    /// Contains various System Constants.
    /// </summary>
    internal class NetworkConst
    {
        /// <summary>
        /// The encoder used for encrypting and decrypting between strings
        /// and the byte arrays sent over the network.
        /// </summary>
        public static readonly UTF8Encoding ENCODER = new UTF8Encoding();

        /// <summary>
        /// The standard port used by communications for this network system.
        /// </summary>
        public const int PORT = 1337; // Obviously

        /// <summary>
        /// The standard buffer size for this network system.
        /// </summary>
        public const int BUFFERSIZE = 1024;
    }

    internal enum MessageType
    {
        System = 0, Action = 1, Response = 2, WaitResponse = 3
    }

}
