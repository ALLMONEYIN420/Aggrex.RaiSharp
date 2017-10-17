using System.Reflection;
using Aggrex.Configuration.Modules;
using Aggrex.ConsensusProtocol.Ioc.Modules;
using Aggrex.Network;
using Aggrex.Network.Modules;
using Autofac;

namespace Aggrex.Application
{
    public class Bootstrapper : IBootstapper
    {
        #region Private Fields 
        private IContainer _container;
        #endregion

        #region Constructor
        public Bootstrapper()
        {
        }
        #endregion

        #region IBootstapper Implementation 
        public void Startup()
        {
            this.RegisterModules();

            this.StartLocalNode();
        }

        public void Shutdown()
        {
        }
        #endregion

        #region Private Methods 
        private void RegisterModules()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<ConsensusProtocolModule>();
            builder.RegisterModule<NetworkModule>();

            this._container = builder.Build();
        }

        public void StartLocalNode()
        {
            using (var scope = this._container.BeginLifetimeScope())
            {
                var node = scope.Resolve<ILocalNode>();
                node.Start();
            }
        }
        #endregion
    }
}
