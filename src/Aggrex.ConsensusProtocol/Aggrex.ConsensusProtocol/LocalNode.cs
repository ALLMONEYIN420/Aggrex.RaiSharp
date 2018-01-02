using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Configuration;
using Aggrex.ConsensusProtocol.Messages;
using Aggrex.Framework;
using Aggrex.Network;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.KeepAlive;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Aggrex.ConsensusProtocol
{
    /// <summary>
    /// Represents a client node in the system. 
    /// </summary>
    internal class LocalNode : ILocalNode
    {
        private readonly INetworkListenerLoop _networkListenerLoop;
        private readonly IUPnPPortForwarder _uPnPPortForwarder;
        private readonly IPeerTracker _peerTracker;
        private readonly IMessageDispatcher _messageDispatcher;
        private readonly ILogger<LocalNode> _logger;
        private readonly ClientSettings _clientSettings;
        private RemoteNode.Factory _remoteNodeFactory { get; set; }

        public LocalNode(
            INetworkListenerLoop networkListenerLoop,
            IUPnPPortForwarder portForwarder,
            ILocalIpAddressDiscoverer localIpAddressDiscoverer,
            RemoteNode.Factory remoteNodeFactory,
            IPeerTracker peerTracker,
            ILoggerFactory loggerFactory,
            IMessageDispatcher messageDispatcher,
            ClientSettings clientSettings)
        {
            _logger = loggerFactory.CreateLogger<LocalNode>();

            _uPnPPortForwarder = portForwarder;
            _peerTracker = peerTracker;

            _networkListenerLoop = networkListenerLoop;
            _networkListenerLoop.TcpConnectionEstablished += HandleConnectionEstablished;
            _networkListenerLoop.DatagramReceived += HandleDatagramReceived;

            _messageDispatcher = messageDispatcher;

            _clientSettings = clientSettings;
            _remoteNodeFactory = remoteNodeFactory;

            int port = _clientSettings.ListenPort;
            LocalAddress = new IPEndPoint(IPAddress.Parse(localIpAddressDiscoverer.GetLocalIpAddress()), port);

            _logger.LogInformation($"Started Listening on {LocalAddress.Address}:{LocalAddress.Port}");
        }

        private void HandleDatagramReceived(object sender, DataGramReceivedArgs args)
        {
            var messageHeader = new MessageHeader();
            using (var reader = new BinaryReader(new MemoryStream(args.Data)))
            {
                if (messageHeader.ReadFromStream(reader))
                {
                    _messageDispatcher.DispatchDatagramMessage(messageHeader, reader , args.Sender);
                }
            }
        }

        public IPEndPoint LocalAddress { get; private set; }

        public void Start()
        {
            Task.Run(async () =>
            {
                await _uPnPPortForwarder.ForwardPortIfNatFound();
                Task.Run(() => _networkListenerLoop.ExecuteTcpListenerLoop());
                Task.Run(() => _networkListenerLoop.ExecuteUdpListenerLoop());
            });

            Task.Run(() =>
            {
                foreach (var peer in _clientSettings.BlockChainNetSettings.SeedPeers)
                {
                    IPAddress[] addresslist = Dns.GetHostAddresses(peer);
                    ConnectToPeer(new IPEndPoint(addresslist[0], _clientSettings.BlockChainNetSettings.Port));
                }
            });


            Task.Run(() => KeepAliveLoop());
        }


        private void KeepAliveLoop()
        {
            while (true)
            {
                if (_peerTracker.NeedsMoreConnectedPeers)
                {
                    foreach (var remoteNode in _peerTracker.GetConnectedPeers())
                    {
                        if (!remoteNode.QueueContainsMessageType<KeepAliveMessage>())
                        {
                            remoteNode.QueueMessage(new KeepAliveMessage());
                        }
                    }

                    foreach (var endpoint in _peerTracker.GetNotConnectedEndPoints(Int32.MaxValue, 0))
                    {
                        Task.Run(() => ConnectToPeer(endpoint));
                    }

                    for (int i = 0; i < 50; i++)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private void ConnectToPeer(IPEndPoint endpoint)
        {
            TcpClient client = new TcpClient();
            client.Connect(endpoint);

            if (client.Connected)
            {
                OnNodeConnectionEstablished(client);
            }
        }

        private void OnNodeConnectionEstablished(TcpClient client)
        {
            IRemoteNode newNode = _remoteNodeFactory.Invoke(client);
            newNode.ExecuteProtocolLoop();
        }

        private void HandleConnectionEstablished(object sender, TcpClient client)
        {
            OnNodeConnectionEstablished(client);
        }
    }
}