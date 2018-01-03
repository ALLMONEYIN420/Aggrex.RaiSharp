using System;
using System.Net;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.Network
{
    /// <summary>
    /// Represents a remote node participating in the network.
    /// </summary>
    public interface IRemoteNode
    {
        IPEndPoint IpEndPoint { get; }
    }
}