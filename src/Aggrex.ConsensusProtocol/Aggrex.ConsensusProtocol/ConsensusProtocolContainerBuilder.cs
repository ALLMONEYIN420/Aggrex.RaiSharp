using System;
using Aggrex.Common;
using Aggrex.ConsensusProtocol.HandShakes;
using Aggrex.ConsensusProtocol.MessageProcessors;
using Aggrex.ConsensusProtocol.MessageProcessors.Addresses;
using Aggrex.ConsensusProtocol.Messages;
using Aggrex.ConsensusProtocol.Messages.Addresses;
using Aggrex.ConsensusProtocol.Transaction;
using Aggrex.ConsensusProtocol.TransactionProcessors;
using Aggrex.Network;
using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.Operations;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.ConsensusProtocol
{
    public static class ConsensusProtocolContainerBuilder
    {
        public static void BuildContainer(ContainerBuilder builder)
        {
            builder.RegisterType<LocalNode>()
                .As<ILocalNode>()
                .SingleInstance();

            builder.RegisterType<MessageDispatcher>()
                .As<IMessageDispatcher>()
                .SingleInstance();

            builder.RegisterType<HandShakeProcessor>()
                .As<IHandShakeProcessor>()
                .SingleInstance();

            builder.RegisterType<PeerAddressesPayloadMessageProcessor>()
                .As<IMessageProcessor<PeerAddressesPayloadMessage>>()
                .SingleInstance();

            builder.RegisterType<RequestPeerAddressMessageProcessor>()
                .As<IMessageProcessor<RequestPeerAddressesMessage>>()
                .SingleInstance();

            builder.RegisterType<TransactionDispatcher>()
                .As<ITransactionDispatcher>()
                .SingleInstance();

            builder.RegisterType<TransferTransactionProcessor>()
                .As<ITransactionProcessor<TransferTransaction>>()
                .SingleInstance();

        }
    }
}