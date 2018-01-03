namespace Aggrex.Configuration
{
    public class ClientSettings
    {
        public byte VersionMax { get; set; }
        public byte VersionMin { get; set; }
        public byte VersionUsing { get; set; }
        public short Extensions { get; set; }
        public string NodeType { get; set; }
        public BlockChainNetSettings BlockChainNetSettings { get; set; }
    }
}