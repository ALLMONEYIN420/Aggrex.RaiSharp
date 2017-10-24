using System.Reflection;
using Aggrex.Configuration.Modules;
using Aggrex.ConsensusProtocol.Ioc.Modules;
using Aggrex.Database.LiteDB.Modules;
using Aggrex.Network;
using Aggrex.Network.Modules;
using Autofac;

namespace Aggrex.Application
{
    public class Bootstrapper : IBootstapper
    {
        private IContainer _container;

        public void Startup()
        {
            RegisterModules();

            StartLocalNode();
        }

        public void Shutdown()
        {
        }

        private void RegisterModules()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<ConsensusProtocolModule>();
            builder.RegisterModule<NetworkModule>();
            builder.RegisterModule<LiteDBModule>();

            _container = builder.Build();
        }

        public void StartLocalNode()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var node = scope.Resolve<ILocalNode>();
                node.Start();
            }
        }
    }
}
