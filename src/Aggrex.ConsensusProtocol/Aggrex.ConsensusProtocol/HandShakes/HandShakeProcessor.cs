using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Configuration;
using Aggrex.ConsensusProtocol.Messages;
using Aggrex.ConsensusProtocol.Messages.HandShake;
using Aggrex.Network;
using Aggrex.Network.HandShakes;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.HandShakes
{
    public class HandShakeProcessor : IHandShakeProcessor
    {
        private readonly ClientSettings _clientSettings;
        private readonly IPeerTracker _peerTracker;
        private readonly IDeterministicNetworkIdGenerator _deterministicNetworkIdGenerator;
        public HandShakeProcessor(
            ClientSettings clientSettings, 
            IPeerTracker peerTracker, 
            IDeterministicNetworkIdGenerator deterministicNetworkIdGenerator)
        {
            _clientSettings = clientSettings;
            _peerTracker = peerTracker;
            _deterministicNetworkIdGenerator = deterministicNetworkIdGenerator;
        }

        public void ProcessHandShake(BinaryReader reader, IRemoteNode remoteNode)
        {
            IntroductionMessage messageToSend = new IntroductionMessage();
            messageToSend.BlockHeight = 100;
            messageToSend.Version = new Version(_clientSettings.Version);
            messageToSend.Port = _clientSettings.BlockChainNetSettings.ListenPortOverride ?? _clientSettings.ListenPort;
            messageToSend.DNID = _deterministicNetworkIdGenerator.GenerateNetworkId;

            remoteNode.QueueMessage(messageToSend);

            MessageType msgType = (MessageType)reader.ReadByte();

            if (msgType != MessageType.Introduction)
            {
                throw new ProtocolViolationException("Invalid version response.");
            }

            IntroductionMessage receivedMessage = new IntroductionMessage();
            receivedMessage.ReadFromStream(reader);

            if (receivedMessage.Version != new Version(_clientSettings.Version))
            {
                throw new ProtocolViolationException("Version mismatch.");
            }

            remoteNode.ListenerEndpoint = new IPEndPoint(remoteNode.RemoteEndPoint.Address, receivedMessage.Port);

            if (_peerTracker.TryAddNewConnectedPeer(remoteNode))
            {
                Console.WriteLine("Successfully Received Introduction Request!");
                Console.WriteLine("Starting Protocol With New Peer.");

                Task.Run(() => remoteNode.ExecuteProtocolLoop());
            }
        }
    }
}