using System;
using Aggrex.Configuration;
using Aggrex.ConsensusProtocol;
using Aggrex.Network;
using Aggrex.ServiceContainer;
using Autofac;

namespace Aggrex.ServiceRegistration
{
    public static class AggrexServiceRegistraction
    {
        public static void RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();

            ConfigurationContainerBuilder.BuildContainer(builder);

            ConsensusProtocolContainerBuilder.BuildContainer(builder);

            NetworkContainerBuilder.BuildContainer(builder);

            AggrexContainer.Container = builder.Build();
        }
    }
}
