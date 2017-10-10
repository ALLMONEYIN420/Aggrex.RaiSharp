using System;
using System.Net;
using Aggrex.Network;
using Aggrex.ServiceContainer;
using Aggrex.ServiceRegistration;
using Autofac;

namespace Aggrex.Client.Cli4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "P4";

            AggrexServiceRegistraction.RegisterServices();

            ILocalNode node = AggrexContainer.Container.Resolve<ILocalNode>();
            node.Start();

            Console.ReadKey();
        }
    }
}
