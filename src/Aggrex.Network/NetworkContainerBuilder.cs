using Aggrex.Network.ObjectReader;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.Network
{
    public static class NetworkContainerBuilder
    {
        public static void BuildContainer(ContainerBuilder builder)
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
        }
    }
}