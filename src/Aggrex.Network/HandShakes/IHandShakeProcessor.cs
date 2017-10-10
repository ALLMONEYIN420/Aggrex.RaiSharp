using System.IO;
using Aggrex.Network.Requests;

namespace Aggrex.Network.HandShakes
{
    /// <summary>
    /// A handshake is the procesing of the first set of messages that are passed between 
    /// two communication nodes on the network. This usually allows for nodes to give information
    /// to each other about protocol version, known peers, etc. 
    /// </summary>
    public interface IHandShakeProcessor
    {
        void ProcessHandShake(BinaryReader reader, IRemoteNode remoteNode);
    }
}