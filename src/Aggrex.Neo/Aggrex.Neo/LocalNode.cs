using System;
using System.Net;
using Aggrex.Configuration;
using Aggrex.Network;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Neo
{
    public class LocalNode : ILocalNode
    {
        public LocalNode(ILocalIpAddressDiscoverer localIpAddressDiscoverer, ClientSettings clientSettings)
        {
        }

        public IPEndPoint LocalAddress { get; }
        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
