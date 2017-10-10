namespace Aggrex.Configuration
{
    public class ClientSettings
    {
        public string Version { get; set; }
        public string NodeType { get; set; }
        public int ListenPort { get; set; }
        public BlockChainNetSettings BlockChainNetSettings { get; set; }
    }
}