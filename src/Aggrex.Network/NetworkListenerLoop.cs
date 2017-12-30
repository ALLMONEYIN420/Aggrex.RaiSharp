using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Aggrex.Configuration;
using Autofac;

namespace Aggrex.Network
{
    public class NetworkListenerLoop : INetworkListenerLoop
    {
        private readonly TcpListener _tcpListener;
        private readonly UdpClient _udpListener;
        private readonly int _port;

        public bool IsBroadCasting { get; set; }
        public NetworkListenerLoop(ClientSettings clientSettings)
        {
            _port = clientSettings.ListenPort;
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _udpListener = new UdpClient();
        }

        public event EventHandler<TcpClient> TcpConnectionEstablished;

        public event EventHandler<byte[]> UdpPacketReceived;

        public void ExecuteTcpListenerLoop()
        {
            _tcpListener.Start();

            while (true)
            {
                TcpClient client = _tcpListener.AcceptTcpClient();
                TcpConnectionEstablished?.Invoke(this, client);
            }
        }

        public void ExecuteUdpListenerLoop()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, _port);
            _udpListener.Client.Bind(endPoint);
            IPEndPoint senderDetails = null;
            while (true)
            {
                var data = _udpListener.Receive(ref senderDetails);
                _udpListener.Receive(ref endPoint);
                UdpPacketReceived?.Invoke(this, data);
            }
        }
    }
}
