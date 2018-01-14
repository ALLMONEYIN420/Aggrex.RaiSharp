using System.IO;
using System.Net;
using Aggrex.Network.Messages.KeepAlive;

namespace Aggrex.Network.Messages.Publish
{
    public class PublishMessageProcessor : IMessageProcessor
    {
        private IPeerTracker _peerTracker;

        public PublishMessageProcessor(IPeerTracker peerTracker)
        {
            _peerTracker = peerTracker;
        }


        public void ProcessUdpMessage(MessageHeader messageHeader, BinaryReader reader, IPEndPoint sender)
        {
            PublishMessage msg = new PublishMessage(messageHeader);

            if (msg.ReadFromStream(reader))
            {
            }
        }
    }
}