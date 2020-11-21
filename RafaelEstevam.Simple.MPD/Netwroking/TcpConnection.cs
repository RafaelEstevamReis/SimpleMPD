using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RafaelEstevam.Simple.MPD.Interfaces;

namespace RafaelEstevam.Simple.MPD.Netwroking
{
    public class TcpConnection : IConnection
    {
        public IPEndPoint EndPoint { get; }
        public TcpClient tcpClient { get; private set; }
        public bool IsConnected
        {
            get
            {
                if (tcpClient == null) return false;
                return tcpClient.Connected;
            }
        }

        public TcpConnection(string Address, int Port = 6600)
            : this(new IPEndPoint(IPAddress.Parse(Address), Port))
        { }
        public TcpConnection(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }

        public async Task< bool> TryConnectAsync()
        {
            if (IsConnected) return true;
            await OpenAsync();
            return IsConnected;
        }

        public async Task OpenAsync()
        {
            if (tcpClient == null) tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(EndPoint.Address, EndPoint.Port);
        }
        public void Open()
        {
            OpenAsync().Wait();
        }

        public void Close()
        {
            if (tcpClient == null) return;
            if (!IsConnected) return;
            tcpClient.Close();
        }

        public Stream GetStream()
        {
            return tcpClient?.GetStream();
        }
    }
}
