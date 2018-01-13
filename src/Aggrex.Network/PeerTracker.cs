using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using Aggrex.Common;
using Aggrex.Configuration;
using Aggrex.Framework;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Aggrex.Network
{
    internal class PeerTracker : IPeerTracker
    {
        private readonly ConcurrentDictionary<string, IRemoteNode> _trackedPeers;

        private readonly int MAX_CONNECTED_PEER_COUNT = 10;

        private readonly int MAX_NOT_CONNECTED_PEER_COUNT = 25;

        private RemoteNode.Factory _remoteNodeFactory;

        private ILogger<PeerTracker> _logger;
        private ClientSettings _clientSettings;
        private IPEndPoint _localIpEndpoint;
        private Random _random;

        public PeerTracker(ILocalIpAddressDiscoverer localIpAddressDiscoverer, ClientSettings clientSettings,RemoteNode.Factory remoteNodeFactory, ILoggerFactory loggerFactory)
        {
            _random = new Random();
            _clientSettings = clientSettings;
            _localIpEndpoint = new IPEndPoint(IPAddress.Parse(localIpAddressDiscoverer.GetLocalIpAddress()), _clientSettings.BlockChainNetSettings.UdpPort);
            _trackedPeers = new ConcurrentDictionary<string, IRemoteNode>();
            _remoteNodeFactory = remoteNodeFactory;
            _logger = loggerFactory.CreateLogger<PeerTracker>();

            foreach (var peer in _clientSettings.BlockChainNetSettings.SeedPeers)
            {
                IPAddress[] addresslist = Dns.GetHostAddresses(peer);
                TryAddPeer(new IPEndPoint(addresslist[0], 7075));
            }
        }

        public bool NeedsMoreTrackedPeers => _trackedPeers.Count < MAX_NOT_CONNECTED_PEER_COUNT;

        public IEnumerable<IRemoteNode> GetRandomSetOfTrackedPeers(int upperLimit)
        {
            List<IRemoteNode> randomList = new List<IRemoteNode>();

            for (int i = 0; i < upperLimit; i++)
            {
                randomList.Add(_trackedPeers.ElementAt(_random.Next(0, _trackedPeers.Count)).Value);
            }

            return randomList.Distinct();
        }

        public IEnumerable<IRemoteNode> GetAllTrackedPeers()
        {
            foreach (var trackedPeer in _trackedPeers)
            {
                yield return trackedPeer.Value;
            }
        }

        public bool TryAddPeer(IPEndPoint endPoint)
        {
            if (Equals(endPoint.Address, _localIpEndpoint.Address)
                && endPoint.Port == _localIpEndpoint.Port)
            {
                return false;
            }

            if (_trackedPeers.TryAdd($"{endPoint.Address}:{endPoint.Port}", _remoteNodeFactory.Invoke(endPoint)))
            {
                _logger.LogDebug("Added new peer {PEER}", endPoint.Address.MapToIPv4().ToString());
                return true;
            }

            return false;
        }

        public bool TryRemovePeer(IPEndPoint endPoint, out IRemoteNode outPeer)
        {
            if (_trackedPeers.TryRemove($"{endPoint.Address}:{endPoint.Port}", out outPeer))
            {
                return true;
            }

            return false;
        }
    }
}