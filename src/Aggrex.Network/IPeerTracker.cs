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
        bool TryAddNewConnectedPeer(IRemoteNode remoteNode);
        bool TryAddNotConnectedIpEndPoint(IPEndPoint listenerEndPoint);
        IRemoteNode RemovePeer(IRemoteNode peer);
        bool NeedsMoreUnConnectedPeers { get; }
        bool NeedsMoreConnectedPeers { get; }
        IRemoteNode[] GetConnectedPeers();
        IPEndPoint[] GetNotConnectedEndPoints(int max, int skip);
        IPEndPoint[] GetConnectedEndPoints(int max, int skip);
    }
}