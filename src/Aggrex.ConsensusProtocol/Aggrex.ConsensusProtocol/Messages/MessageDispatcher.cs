﻿using System.IO;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.ConsensusProtocol.TransactionProcessors;
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

        public void DispatchProtocolMessage(MessageType messageType, BinaryReader reader, IRemoteNode remoteNode)
        {
            switch (messageType)
            {
                case MessageType.RequestPeerAddresses:
                    _messageProcessors[MessageType.RequestPeerAddresses].ProcessMessage(reader, remoteNode);
                    break;

                case MessageType.PeerAddressesPayload:
                    _messageProcessors[MessageType.PeerAddressesPayload].ProcessMessage(reader, remoteNode);
                    break;

                case MessageType.Transaction:
                    _transactionDispatcher.DispatchTransaction(reader, remoteNode);
                    break;
            }
        }
    }
}