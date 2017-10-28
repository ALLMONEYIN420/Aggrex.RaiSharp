using System;
using System.IO;
using Aggrex.ConsensusProtocol.Messages.ActiveNodeSet;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.Framework;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.ObjectReader;

namespace Aggrex.ConsensusProtocol.MessageProcessors.ActiveNodeSet
{
    public class ActiveNodeSetPayloadMessageProcessor : IMessageProcessor
    {
        private IObjectReader _objectReader;
        private IActiveNodeSet _activeNodeSet;

        public ActiveNodeSetPayloadMessageProcessor(IObjectReader objectReader, IActiveNodeSet activeNodeSet)
        {
            _objectReader = objectReader;
            _activeNodeSet = activeNodeSet;
        }

        public void ProcessMessage(ActiveNodeSetPayloadMessage message, IRemoteNode remoteNode)
        {
            foreach (var keyValuePair in message.Confirmations)
            {
                _activeNodeSet.Add(keyValuePair.Key);
            }
        }

        public void ProcessMessage(BinaryReader reader, IRemoteNode remoteNode)
        {
            ProcessMessage(_objectReader.ReadObject<ActiveNodeSetPayloadMessage>(reader), remoteNode);
        }
    }
}