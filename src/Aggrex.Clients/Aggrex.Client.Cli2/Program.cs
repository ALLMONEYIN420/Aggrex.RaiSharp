using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Network;
using Aggrex.Network.Requests;
using Aggrex.ServiceContainer;
using Aggrex.ServiceRegistration;
using Autofac;

namespace Aggrex.Client.Cli2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "P2";

            AggrexServiceRegistraction.RegisterServices();

            ILocalNode node = AggrexContainer.Container.Resolve<ILocalNode>();
            node.Start();

            Console.ReadKey();
        }
    }
}
