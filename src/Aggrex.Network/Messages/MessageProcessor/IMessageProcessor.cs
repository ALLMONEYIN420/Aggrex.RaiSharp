using System.IO;

namespace Aggrex.Network.Messages.MessageProcessor
{
    /// <summary>
    /// Message processers are used to process specific types of messages that are passed on the 
    /// network between nodes.
    /// </summary>
    public interface IMessageProcessor
    {
        void ProcessMessage(BinaryReader reader, IRemoteNode remoteNode);
    }
}