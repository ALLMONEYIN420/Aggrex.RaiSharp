using System.IO;
using System.Net;

namespace Aggrex.Network.Messages.MessageProcessor
{
    /// <summary>
    /// Message processers are used to process specific types of messages that are passed on the 
    /// network between nodes.
    /// </summary>
    public interface IMessageProcessor
    {
        void ProcessTcpMessage(BinaryReader reader, IRemoteNode remoteNode);
        void ProcessUdpMessage(byte[] data, IPEndPoint sender);
    }
}