using System;
using Aggrex.Application;

namespace Aggrex.Client.Cli2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "P2";

            var networkBootstrapper = new Bootstrapper();
            networkBootstrapper.Startup();

            Console.ReadKey();
        }
    }
}
