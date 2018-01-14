using Aggrex.Common.BitSharp;

namespace Aggrex.Database.Models
{
    public class ChangeBlocks 
    {
        public UInt256 BlockHash { get; set; }
        public byte[] ValueBytes { get; set; }
    }
}