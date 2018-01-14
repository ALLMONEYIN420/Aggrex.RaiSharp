using Aggrex.Common.BitSharp;

namespace Aggrex.Database.Models
{
    public class OpenBlocksDb
    {
        public UInt256 BlockHash { get; set; }
        public UInt256 OpenBlock { get; set; }
    }
}