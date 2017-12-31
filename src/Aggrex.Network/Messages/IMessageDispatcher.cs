using System.IO;
using System.Net;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    /// <summary>
    /// The job of this interface is to be able to find an <see cref="IMessageProcessor{T}"/>
    /// when given a message type. This is usually used after reading network packets.
    /// </summary>
    public interface IMessageDispatcher
    {
        void DispatchTcpProtocolMessage(MessageType messageType, BinaryReader reader, IRemoteNode remoteNode);
        void DispatchDatagramMessage(MessageType messageType, byte[] data, IPEndPoint sender);
    }
}