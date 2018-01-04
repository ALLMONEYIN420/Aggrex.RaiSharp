using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.ObjectReader;
using Aggrex.Network.Packets;
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
        }
    }
}

