using System;
using Aggrex.ConsensusProtocol.Messages;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;

namespace Aggrex.ConsensusProtocol.MessageProcessors.Addresses
{
    public class RequestPeerAddressMessageProcessor : IMessageProcessor<RequestPeerAddressesMessage>
    {
        private readonly IPeerTracker _peerTracker;
        public RequestPeerAddressMessageProcessor(IPeerTracker peerTracker)
        {
            _peerTracker = peerTracker;
        }

        public void ProcessMessage(RequestPeerAddressesMessage message, IRemoteNode remoteNode)
        {
            PeerAddressesPayloadMessage response = new PeerAddressesPayloadMessage();
            response.NotConnectedIpEndPoints = _peerTracker.GetNotConnectedEndPoints(Int32.MaxValue, 0);
            response.ConnectedIpEndPoints = _peerTracker.GetConnectedEndPoints(Int32.MaxValue, 0);
            remoteNode.QueueMessage(response);
        }
    }
}