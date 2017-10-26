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
        IPEndPoint RemoteEndPoint { get;  }

        IPEndPoint ListenerEndpoint { get; set; }

        void ExecuteProtocolLoop();

        void ExecuteProtocolHandShake();

        void QueueMessage(BaseMessage message);

        string DNID { get; set; }

        bool QueueContainsMessageType<T>() where T : BaseMessage;

        event EventHandler Disconnected;
    }
}