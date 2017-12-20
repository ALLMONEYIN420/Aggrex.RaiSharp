using System;
using Aggrex.Application;

namespace Aggrex.RaiNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "RaiNode";

            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            Console.ReadKey();
        }
    }
}
