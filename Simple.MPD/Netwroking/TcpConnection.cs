using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Simple.MPD.Interfaces;

namespace Simple.MPD.Netwroking
{
    /// <summary>
    /// Implements IConnection with TCP
    /// </summary>
    public class TcpConnection : IConnection
    {
        private StreamReader reader;
        private StreamWriter writer;
        /// <summary>
        /// Gets the endpoint
        /// </summary>
        public IPEndPoint EndPoint { get; }
        /// <summary>
        /// Gets TCP Client
        /// </summary>
        public TcpClient TcpClient { get; private set; }
        /// <summary>
        /// Gets if the connection is active
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (TcpClient == null) return false;
                return TcpClient.Connected;
            }
        }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public TcpConnection(string Address, int Port = 6600)
            : this(createEndPoint(Address, Port))
        { }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public TcpConnection(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }
        /// <summary>
        /// Tries to connect
        /// </summary>
        /// <returns></returns>
        public async Task< bool> TryConnectAsync()
        {
            if (IsConnected) return true;
            await OpenAsync();
            return IsConnected;
        }
        /// <summary>
        /// Opens the connection
        /// </summary>
        public async Task OpenAsync()
        {
            if (TcpClient == null || TcpClient.Client == null) TcpClient = new TcpClient();
            await TcpClient.ConnectAsync(EndPoint.Address, EndPoint.Port);

#if NETSTANDARD
            reader = new StreamReader(TcpClient.GetStream(), Encoding.Default, false, 512, leaveOpen: true);
            writer = new StreamWriter(TcpClient.GetStream(), Encoding.Default, 512, leaveOpen: true);
#elif NET45
            reader = new StreamReader(TcpClient.GetStream());
            writer = new StreamWriter(TcpClient.GetStream());
#else
            reader = new StreamReader(TcpClient.GetStream(), leaveOpen: true);
            writer = new StreamWriter(TcpClient.GetStream(), leaveOpen: true);
#endif

            writer.AutoFlush = true;

        }
        /// <summary>
        /// Opens the connection
        /// </summary>
        public void Open()
        {
            OpenAsync().Wait();
        }
        /// <summary>
        /// Closes the connection
        /// </summary>
        public void Close()
        {
            if (TcpClient == null) return;
            if (!IsConnected) return;
            TcpClient.Close();
        }

        /// <summary>
        /// Get reader stream
        /// </summary>
        public StreamReader GetReader()
        {
            return reader;
        }
        /// <summary>
        /// Get writter stream
        /// </summary>
        public StreamWriter GetWriter()
        {
            return writer;
        }

        private static IPEndPoint createEndPoint(string address, int port)
        {
            if (address.Split('.').Length == 4)
            {
                if (IPAddress.TryParse(address, out IPAddress ip))
                    return new IPEndPoint(ip, port);
            }
            var addresses = System.Net.Dns.GetHostAddresses(address);
            if (addresses.Length == 0) throw new Exception($"Invalid address: '{address}'");

            return new IPEndPoint(addresses[0], port);
        }

    }
}
