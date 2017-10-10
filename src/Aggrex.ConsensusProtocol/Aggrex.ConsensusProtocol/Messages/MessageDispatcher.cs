using System.IO;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.ConsensusProtocol.Transaction;
using Aggrex.Network;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.ObjectReader;
using Aggrex.Network.Requests;
using Aggrex.ServiceContainer;
using Autofac;

namespace Aggrex.ConsensusProtocol.Messages
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IObjectReader _reader;
        private readonly ITransactionDispatcher _transactionDispatcher;

        public MessageDispatcher(IObjectReader reader, ITransactionDispatcher transactionDispatcher)
        {
            _reader = reader;
            _transactionDispatcher = transactionDispatcher;
        }

        public void DispatchProtocolMessage(MessageType messageType, BinaryReader reader, IRemoteNode remoteNode)
        {
            switch (messageType)
            {
                case MessageType.GetPeerAddresses:
                    DispatchMessage<RequestPeerAddressesMessage>(reader, remoteNode);
                    break;

                case MessageType.PeerAddressesPayload:
                    DispatchMessage<PeerAddressesPayloadMessage>(reader, remoteNode);
                    break;

                case MessageType.Transaction:
                    _transactionDispatcher.DispatchTransaction(reader, remoteNode);
                    break;
            }
        }

        private void DispatchMessage<T>(BinaryReader reader, IRemoteNode remoteNode) where T : BaseMessage, new()
        {
            var request = _reader.ReadObject<T>(reader);
            var processor = AggrexContainer.Container.Resolve<IMessageProcessor<T>>();
            processor.ProcessMessage(request, remoteNode);
        }
    }
}