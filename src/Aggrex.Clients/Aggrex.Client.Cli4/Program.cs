using System;
using System.Net;
using Aggrex.Application;
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

            var networkBootstrapper = new Bootstrapper();
            networkBootstrapper.Startup();

            Console.ReadKey();
        }
    }
}
