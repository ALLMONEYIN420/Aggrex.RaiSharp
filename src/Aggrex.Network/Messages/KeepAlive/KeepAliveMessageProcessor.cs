using System.IO;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Aggrex.Network.Messages.KeepAlive
{
    public class KeepAliveMessageProcessor : IMessageProcessor
    {
        private IPeerTracker _peerTracker;

        public KeepAliveMessageProcessor(IPeerTracker peerTracker)
        {
            _peerTracker = peerTracker;
        }

        public void ProcessUdpMessage(MessageHeader messageHeader, BinaryReader reader, IPEndPoint sender)
        {
            KeepAliveMessage msg = new KeepAliveMessage();
            if (msg.ReadFromStream(reader))
            {
                _peerTracker.TryAddPeer(sender);
            }
        }
    }
}