using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Configuration.Modules;
using Aggrex.ConsensusProtocol.Ioc.Modules;
using Aggrex.Network;
using Aggrex.Network.Modules;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.Client.Cli2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "P2";

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
