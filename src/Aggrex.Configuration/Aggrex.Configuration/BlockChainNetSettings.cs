using System;

namespace Aggrex.Configuration
{
    public class BlockChainNetSettings
    {
        public string Net { get; set; }
        public int Port { get; set; }
        public string[] SeedPeers { get; set; }
    }
}