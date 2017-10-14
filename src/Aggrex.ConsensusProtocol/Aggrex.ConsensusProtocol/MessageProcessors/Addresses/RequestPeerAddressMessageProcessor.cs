using System;
using System.IO;
using Aggrex.ConsensusProtocol.Messages;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.ObjectReader;

namespace Aggrex.ConsensusProtocol.MessageProcessors.Addresses
{
    public class RequestPeerAddressMessageProcessor : IMessageProcessor
    {
        private readonly IPeerTracker _peerTracker;
        private readonly IObjectReader _objectReader;
        public RequestPeerAddressMessageProcessor(IObjectReader objectReader, IPeerTracker peerTracker)
        {
            _objectReader = objectReader;
            _peerTracker = peerTracker;
        }

        public void ProcessMessage(RequestPeerAddressesMessage message, IRemoteNode remoteNode)
        {
            PeerAddressesPayloadMessage response = new PeerAddressesPayloadMessage();
            response.NotConnectedIpEndPoints = _peerTracker.GetNotConnectedEndPoints(Int32.MaxValue, 0);
            response.ConnectedIpEndPoints = _peerTracker.GetConnectedEndPoints(Int32.MaxValue, 0);
            remoteNode.QueueMessage(response);
        }

        public void ProcessMessage(BinaryReader reader, IRemoteNode remoteNode)
        {
            ProcessMessage(_objectReader.ReadObject<RequestPeerAddressesMessage>(reader), remoteNode);
        }
    }
}