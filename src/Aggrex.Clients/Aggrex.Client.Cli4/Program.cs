using System;
using System.Net;
using Aggrex.Configuration.Modules;
using Aggrex.ConsensusProtocol.Ioc.Modules;
using Aggrex.Network;
using Aggrex.Network.Modules;
using Autofac;

namespace Aggrex.Client.Cli4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "P4";

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<ConsensusProtocolModule>();
            builder.RegisterModule<NetworkModule>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                ILocalNode node = scope.Resolve<ILocalNode>();
                node.Start();

                Console.ReadKey();
            }
        }
    }
}
