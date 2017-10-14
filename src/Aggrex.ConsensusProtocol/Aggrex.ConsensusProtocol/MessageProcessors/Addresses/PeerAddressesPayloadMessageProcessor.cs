using System;
using System.IO;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.ObjectReader;

namespace Aggrex.ConsensusProtocol.MessageProcessors.Addresses
{
    public class PeerAddressesPayloadMessageProcessor : IMessageProcessor
    {
        private readonly IPeerTracker _peerTracker;
        private IObjectReader _objectReader;

        public PeerAddressesPayloadMessageProcessor(IObjectReader objectReader, IPeerTracker peerTracker)
        {
            _objectReader = objectReader;
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

        public void ProcessMessage(BinaryReader reader, IRemoteNode remoteNode)
        {
            ProcessMessage(_objectReader.ReadObject<PeerAddressesPayloadMessage>(reader), remoteNode);
        }
    }
}