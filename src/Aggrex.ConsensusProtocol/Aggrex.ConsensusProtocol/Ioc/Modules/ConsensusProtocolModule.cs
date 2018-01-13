using Aggrex.ConsensusProtocol.Messages;
using Aggrex.Framework;
using Aggrex.Network;
using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.KeepAlive;
using Aggrex.Network.Messages.Publish;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.ConsensusProtocol.Ioc.Modules
{
    public class ConsensusProtocolModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalNode>()
                .As<ILocalNode>()
                .SingleInstance();

            builder.RegisterType<MessageDispatcher>()
                .As<IMessageDispatcher>()
                .SingleInstance();

            builder.RegisterType<KeepAliveMessageProcessor>()
                .SingleInstance().Keyed<IMessageProcessor>(MessageType.Keepalive);

            builder.RegisterType<PublishMessageProcessor>()
                .SingleInstance().Keyed<IMessageProcessor>(MessageType.Publish);

        }
    }
}

