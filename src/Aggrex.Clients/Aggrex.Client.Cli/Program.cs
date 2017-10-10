using Aggrex.Network;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aggrex.ServiceContainer;
using Aggrex.ServiceRegistration;
using Autofac;

namespace Aggrex.Client.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Seed Peer";

            AggrexServiceRegistraction.RegisterServices();

            ILocalNode node = AggrexContainer.Container.Resolve<ILocalNode>();
            node.Start();

            Console.ReadKey();
        }
    }
}
