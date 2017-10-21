using System;
using Aggrex.Application;

namespace Aggrex.Client.Cli3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "P3";

            var networkBootstrapper = new Bootstrapper();
            networkBootstrapper.Startup();

            Console.ReadKey();
        }
    }
}
