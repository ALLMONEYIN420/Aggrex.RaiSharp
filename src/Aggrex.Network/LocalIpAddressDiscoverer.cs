using System;
using System.Net;
using System.Net.Sockets;

namespace Aggrex.Network
{
    public class LocalIpAddressDiscoverer : ILocalIpAddressDiscoverer
    {
        public string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}