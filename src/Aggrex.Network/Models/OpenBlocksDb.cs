using Aggrex.Common.BitSharp;
using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network.Models
{
    public class OpenBlocksDb
    {
        public UInt256 BlockHash { get; set; }
        public OpenBlock OpenBlock { get; set; }
    }
}