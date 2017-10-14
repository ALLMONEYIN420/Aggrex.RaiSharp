using Aggrex.Network;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aggrex.Configuration.Modules;
using Aggrex.ConsensusProtocol.Ioc.Modules;
using Aggrex.Network.Modules;
using Autofac;

namespace Aggrex.Client.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Seed Peer";

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
