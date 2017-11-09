using System;
using System.Net;
using Aggrex.Configuration;
using Aggrex.Network;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Neo
{
    public class LocalNode : ILocalNode
    {
        private IPeerTracker _peerTracker;
        private ILocalIpAddressDiscoverer _localIpAddressDiscoverer;
        private string[] _seedPeers;
        public LocalNode(
            ILocalIpAddressDiscoverer localIpAddressDiscoverer,
            IPeerTracker peerTracker,
            ClientSettings clientSettings)
        {
            _localIpAddressDiscoverer = localIpAddressDiscoverer;
            _peerTracker = peerTracker;
            _seedPeers = clientSettings.BlockChainNetSettings.SeedPeers;

            int port = clientSettings.BlockChainNetSettings?.ListenPortOverride ?? clientSettings.ListenPort;
            LocalAddress = new IPEndPoint(IPAddress.Parse(localIpAddressDiscoverer.GetLocalIpAddress()), port);
        }

        public IPEndPoint LocalAddress { get; }
        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
