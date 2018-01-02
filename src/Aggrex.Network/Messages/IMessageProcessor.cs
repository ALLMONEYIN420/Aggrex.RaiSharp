using System.IO;
using System.Net;

namespace Aggrex.Network.Messages
{
    /// <summary>
    /// Message processers are used to process specific types of messages that are passed on the 
    /// network between nodes.
    /// </summary>
    public interface IMessageProcessor
    {
        void ProcessUdpMessage(MessageHeader messageHeader, BinaryReader reader, IPEndPoint sender);
    }
}