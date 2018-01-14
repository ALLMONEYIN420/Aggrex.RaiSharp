using Aggrex.ConsensusProtocol;
using Aggrex.ConsensusProtocol.Messages;
using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.KeepAlive;
using Aggrex.Network.Messages.Publish;
using Aggrex.Network.ObjectReader;
using Aggrex.Network.Packets;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.Network.Modules
{
    public class NetworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NetworkListenerLoop>()
                .As<INetworkListenerLoop>()
                .SingleInstance();

            builder.RegisterType<PeerTracker>()
                .As<IPeerTracker>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<RemoteNode>()
                .As<IRemoteNode>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<UPnPPortForwarder>()
                .As<IUPnPPortForwarder>()
                .SingleInstance();

            builder.RegisterType<ObjectReader.ObjectReader>()
                .As<IObjectReader>()
                .SingleInstance();

            builder.RegisterType<LocalIpAddressDiscoverer>()
                .As<ILocalIpAddressDiscoverer>()
                .SingleInstance();

            builder.RegisterType<PacketSender>()
                .As<IPacketSender>()
                .SingleInstance();

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

