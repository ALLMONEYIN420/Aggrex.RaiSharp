using Aggrex.Common.BitSharp;

namespace Aggrex.Database.Models
{
    public class SendBlocksDb 
    {
        public UInt256 BlockHash { get; set; }
        public UInt256 Account { get; set; }
    }
}