using System;
using Aggrex.Application;

namespace Aggrex.Client.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Seed Peer";

            var networkBootstrapper = new Bootstrapper();
            networkBootstrapper.Startup();

            Console.ReadKey();
        }
    }
}
