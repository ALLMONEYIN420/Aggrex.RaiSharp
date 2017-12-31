using System.IO;
using System.Net;
using Aggrex.ConsensusProtocol.Transactions.Dispatcher;
using Aggrex.Network;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.ObjectReader;
using Aggrex.Network.Requests;
using Autofac;
using Autofac.Features.Indexed;

namespace Aggrex.ConsensusProtocol.Messages
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly ITransactionDispatcher _transactionDispatcher;
        private readonly IIndex<MessageType, IMessageProcessor> _messageProcessors;

        public MessageDispatcher(ITransactionDispatcher transactionDispatcher,
        IIndex<MessageType, IMessageProcessor> messageProcessors)
        {
            _transactionDispatcher = transactionDispatcher;
            _messageProcessors = messageProcessors;
        }

        public void DispatchTcpProtocolMessage(MessageType messageType, BinaryReader reader, IRemoteNode remoteNode)
        {
            switch (messageType)
            {
                case MessageType.Keepalive:
                    _messageProcessors[MessageType.Keepalive].ProcessTcpMessage(reader, remoteNode);
                    break;
            }
        }

        public void DispatchDatagramMessage(MessageType messageType, byte[] data, IPEndPoint sender)
        {
            switch (messageType)
            {
                case MessageType.Keepalive:
                    _messageProcessors[MessageType.Keepalive].ProcessUdpMessage(data, sender);
                    break;
            }
        }
    }
}