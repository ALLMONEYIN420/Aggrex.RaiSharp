using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        public NetworkListenerLoop(ClientSettings clientSettings, IUPnPPortForwarder portFrForwarder)
        {
            _udpPort = clientSettings.BlockChainNetSettings.UdpPort;
            //_tcpListener = new TcpListener(IPAddress.Any, clientSettings.BlockChainNetSettings.TcpPort);
            _udpListener = new UdpClient(new IPEndPoint(IPAddress.Parse("192.168.1.126"), 7075));
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
            while (true)
            {
                try
                {
                    var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    var data = _udpListener.Receive(ref remoteEndPoint);
                    DatagramReceived?.Invoke(this, new DataGramReceivedArgs
                    {
                        Data = data,
                        Sender = remoteEndPoint,
                    });
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
