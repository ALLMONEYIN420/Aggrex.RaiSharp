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
    public class RequestActiveNodeSetMessageProcessor : IMessageProcessor
    {
        private readonly IObjectReader _objectReader;
        private readonly IActiveNodeSet _activeNodeSet;

        public RequestActiveNodeSetMessageProcessor(IObjectReader objectReader, IActiveNodeSet activeNodeSet)
        {
            _objectReader = objectReader;
            _activeNodeSet = activeNodeSet;
        }

        public void ProcessMessage(ActiveNodeSetPayloadMessage message, IRemoteNode remoteNode)
        {
            ActiveNodeSetPayloadMessage response = new ActiveNodeSetPayloadMessage();
            response.Confirmations = _activeNodeSet.Confirmations;
            remoteNode.QueueMessage(response);
        }

        public void ProcessMessage(BinaryReader reader, IRemoteNode remoteNode)
        {
            ProcessMessage(_objectReader.ReadObject<ActiveNodeSetPayloadMessage>(reader), remoteNode);
        }
    }
}