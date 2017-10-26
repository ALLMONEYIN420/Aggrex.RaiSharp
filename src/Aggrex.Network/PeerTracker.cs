using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Aggrex.Common;
using Aggrex.Configuration;
using Aggrex.Framework;
using Autofac;

namespace Aggrex.Network
{
    internal class PeerTracker : IPeerTracker
    {
        private readonly ConcurrentDictionary<string, IRemoteNode> _connectedPeers;

        private readonly HashSet<IPEndPoint> _notConnectedPeers;

        private readonly ClientSettings _clientSettings;

        private readonly IActiveNodeSet _activeNodeSet;

        private readonly int MAX_CONNECTED_PEER_COUNT = 10;

        private readonly int MAX_NOT_CONNECTED_PEER_COUNT = 25;

        public bool NeedsMoreUnConnectedPeers
        {
            get
            {
                return _notConnectedPeers.Count < MAX_NOT_CONNECTED_PEER_COUNT;
            }
        }

        public bool NeedsMoreConnectedPeers
        {
            get
            {
                return _notConnectedPeers.Count < MAX_CONNECTED_PEER_COUNT;
            }
        }

        public IRemoteNode[] GetConnectedPeers()
        {
            lock (_connectedPeers)
            {
                return _connectedPeers.Values.ToArray();
            }
        }

        public IPEndPoint[] GetNotConnectedEndPoints(int max, int skip)
        {
            return _notConnectedPeers.ToArray();
        }

        public IPEndPoint[] GetConnectedEndPoints(int max, int skip)
        {
            lock (_connectedPeers)
            {
                return _connectedPeers.Select(x => x.Value.ListenerEndpoint).ToArray();
            }
        }

        public ILocalNode LocalNode { get; set; }

        public PeerTracker(ClientSettings clientSettings, IActiveNodeSet activeNodeSet)
        {
            _clientSettings = clientSettings;

            _connectedPeers = new ConcurrentDictionary<string, IRemoteNode>();
            _notConnectedPeers = new HashSet<IPEndPoint>();

            _activeNodeSet = activeNodeSet;
        }

        public bool TryAddNewConnectedPeer(IRemoteNode peer)
        {
            if (Equals(peer.ListenerEndpoint.Address, LocalNode.LocalAddress.Address)
                && peer.ListenerEndpoint.Port == (_clientSettings.BlockChainNetSettings?.ListenPortOverride ??_clientSettings.ListenPort))
            {
                return false;
            }

            _activeNodeSet.Add(peer.DNID);


            lock (_notConnectedPeers)
            {
                var unConnectedEntry = _notConnectedPeers.FirstOrDefault(x => Equals(x.Address, peer.ListenerEndpoint.Address) && (x.Port == peer.ListenerEndpoint.Port));
                if (unConnectedEntry != null)
                {
                    _notConnectedPeers.Remove(unConnectedEntry);
                }
            }

            lock (_connectedPeers)
            {
                if (!_connectedPeers.ContainsKey($"{peer.ListenerEndpoint.Address}:{peer.ListenerEndpoint.Port}"))
                {
                    _connectedPeers[$"{peer.ListenerEndpoint.Address}:{peer.ListenerEndpoint.Port}"] = peer;
                    return true;
                }
                return false;
            }
        }

        public bool TryAddNotConnectedIpEndPoint(IPEndPoint listenerEndPoint)
        {
            if (Equals(listenerEndPoint.Address, LocalNode.LocalAddress.Address)
                && listenerEndPoint.Port == (_clientSettings.BlockChainNetSettings?.ListenPortOverride ?? _clientSettings.ListenPort))
            {
                return false;
            }

            lock (_connectedPeers)
            {
                if (_connectedPeers.Any(x => Equals(x.Value.ListenerEndpoint.Address, listenerEndPoint.Address) && x.Value.ListenerEndpoint.Port == listenerEndPoint.Port))
                {
                    return false;
                }
            }

            lock (_notConnectedPeers)
            {
                if (!_notConnectedPeers.Any(x => Equals(x.Address, listenerEndPoint.Address) && x.Port == listenerEndPoint.Port))
                {
                    _notConnectedPeers.Add(listenerEndPoint);
                    return true;
                }
            }

            return false;
        }

        public IRemoteNode RemovePeer(IRemoteNode peer)
        {
            lock (peer)
            {
                IRemoteNode outPeer;
                if (_connectedPeers.TryRemove($"{peer.ListenerEndpoint.Address}:{peer.ListenerEndpoint.Port}", out outPeer))
                {
                    return outPeer;
                }

                return null;
            }
        }
    }
}