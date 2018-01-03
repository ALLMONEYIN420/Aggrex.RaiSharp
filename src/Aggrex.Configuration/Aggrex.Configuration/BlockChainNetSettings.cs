using System;

namespace Aggrex.Configuration
{
    public class BlockChainNetSettings
    {
        public string Net { get; set; }
        public int UdpPort { get; set; }
        public int TcpPort { get; set; }
        public string[] SeedPeers { get; set; }
    }
}