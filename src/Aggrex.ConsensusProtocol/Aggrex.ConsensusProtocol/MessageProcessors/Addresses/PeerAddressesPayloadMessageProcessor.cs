using System;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;

namespace Aggrex.ConsensusProtocol.MessageProcessors.Addresses
{
    public class PeerAddressesPayloadMessageProcessor : IMessageProcessor<PeerAddressesPayloadMessage>
    {
        private readonly IPeerTracker _peerTracker;
        public PeerAddressesPayloadMessageProcessor(IPeerTracker peerTracker)
        {
            _peerTracker = peerTracker;
        }

        public void ProcessMessage(PeerAddressesPayloadMessage message, IRemoteNode remoteNode)
        {
            foreach (var responseIpEndPoint in message.NotConnectedIpEndPoints)
            {
                if (_peerTracker.TryAddNotConnectedIpEndPoint(responseIpEndPoint))
                {
                    Console.WriteLine($"Added new potential peer at: {responseIpEndPoint.Address}:{responseIpEndPoint.Port}");
                }
            }

            foreach (var responseIpEndPoint in message.ConnectedIpEndPoints)
            {
                if (_peerTracker.TryAddNotConnectedIpEndPoint(responseIpEndPoint))
                {
                    Console.WriteLine($"Added new verified connected peer at: {responseIpEndPoint.Address}:{responseIpEndPoint.Port}");
                }
            }
        }
    }
}