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
        private int _udpPort;

        public bool IsBroadCasting { get; set; }
        public NetworkListenerLoop(ClientSettings clientSettings)
        {
            _udpPort = clientSettings.BlockChainNetSettings.UdpPort;
            //_tcpListener = new TcpListener(IPAddress.Any, clientSettings.BlockChainNetSettings.TcpPort);
            _udpListener = new UdpClient();
        }

        public event EventHandler<TcpClient> TcpConnectionEstablished;

        public event EventHandler<DataGramReceivedArgs> DatagramReceived;

        public void ExecuteTcpListenerLoop()
        {
            //_tcpListener.Start();

            //while (true)
            //{
            //    TcpClient client = _tcpListener.AcceptTcpClient();
            //    TcpConnectionEstablished?.Invoke(this, client);
            //}
        }

        public void ExecuteUdpListenerLoop()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, _udpPort);
            _udpListener.Client.Bind(endPoint);
            IPEndPoint senderDetails = null;
            while (true)
            {
                var data = _udpListener.Receive(ref senderDetails);
                _udpListener.Receive(ref endPoint);
                DatagramReceived?.Invoke(this, new DataGramReceivedArgs
                {
                    Data = data,
                    Sender = senderDetails,
                });
            }
        }
    }
}
