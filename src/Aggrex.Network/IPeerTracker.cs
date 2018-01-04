using System.Collections.Generic;
using System.Net;

namespace Aggrex.Network
{
    /// <summary>
    /// This interface is responsible for keeping track of peers. The two main things it tracks
    /// are peers that we have active connections with, and a list of potential peers that we 
    /// may connect to.
    /// </summary>
    public interface IPeerTracker
    {
        bool TryAddPeer(IPEndPoint peer);
        bool TryRemovePeer(IPEndPoint peer, out IRemoteNode remoteNode);
        bool NeedsMoreTrackedPeers { get; }
        IEnumerable<IRemoteNode> GetAllTrackedPeers();
        IEnumerable<IRemoteNode> GetRandomSetOfTrackedPeers(int upperLimit);
    }
}