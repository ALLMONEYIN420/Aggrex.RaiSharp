using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Aggrex.Configuration;
using Aggrex.ServiceContainer;
using Autofac;

namespace Aggrex.Network
{
    public class NetworkListenerLoop : INetworkListenerLoop
    {
        private readonly TcpListener _listener;

        public bool IsBroadCasting { get; set; }
        public NetworkListenerLoop(ClientSettings clientSettings)
        {
            int port = clientSettings.BlockChainNetSettings.ListenPortOverride ?? clientSettings.ListenPort;
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public event EventHandler<TcpClient> ConnectionEstablished;

        public void StartListeningForConnections()
        {
            _listener.Start();

            while (true)
            {
                TcpClient client = _listener.AcceptTcpClient();
                ConnectionEstablished?.Invoke(this, client);
            }
        }
    }
}
