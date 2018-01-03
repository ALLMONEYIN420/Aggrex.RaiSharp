using System.IO;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Aggrex.Network.Messages.KeepAlive
{
    public class KeepAliveMessageProcessor : IMessageProcessor
    {
        private ILogger<KeepAliveMessageProcessor> _logger;
        private IPeerTracker _peerTracker;

        public KeepAliveMessageProcessor(ILoggerFactory loggerFactory, IPeerTracker peerTracker)
        {
            _peerTracker = peerTracker;
            _logger = loggerFactory.CreateLogger<KeepAliveMessageProcessor>();
        }

        public void ProcessUdpMessage(MessageHeader messageHeader, BinaryReader reader, IPEndPoint sender)
        {
            KeepAliveMessage msg = new KeepAliveMessage();
            if (msg.ReadFromStream(reader))
            {
                _logger.LogDebug("Received keepalive message from {SENDER}", sender.Address.MapToIPv4().ToString());
            }

            _peerTracker.TryAddPeer(sender);
        }
    }
}