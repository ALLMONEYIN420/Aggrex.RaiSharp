using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using Aggrex.Configuration;

namespace Aggrex.Network
{
    class DeterministicNetworkIdGenerator : IDeterministicNetworkIdGenerator
    {
        private ClientSettings _clientSettings;
        private ILocalIpAddressDiscoverer _localIpAddressDiscoverer;

        public DeterministicNetworkIdGenerator(ILocalIpAddressDiscoverer localIpAddressDiscoverer,
            ClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
            _localIpAddressDiscoverer = localIpAddressDiscoverer;
        }

        public string GenerateNetworkId
        {
            get
            {
                string macAddress = GetMacAddress();
                string ipAddress = GetIpAddress();
                string clientPort = GetClientPort();

                string key = macAddress + ipAddress + clientPort;

                using (var sha256 = SHA256.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                    return Convert.ToBase64String(hashedBytes);
                }
            }
        }
        private string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            var nicToUse = nics.FirstOrDefault();
            return nicToUse.GetPhysicalAddress().ToString();
        }

        private string GetClientPort()
        {
            int port = _clientSettings.BlockChainNetSettings?.ListenPortOverride ?? _clientSettings.ListenPort;
            return port.ToString();
        }

        private string GetIpAddress()
        {
            return _localIpAddressDiscoverer.GetLocalIpAddress();
        }
    }
}
