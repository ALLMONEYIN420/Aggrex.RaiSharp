using Aggrex.Common.BitSharp;
using Aggrex.Network.Messages.Publish.Blocks;
using LiteDB;

namespace Aggrex.Network.Models
{
    public class SendBlocksDb 
    {
        [BsonId]
        public UInt256 BlockHash { get; set; }
        public SendBlock SendBlock { get; set; }
    }
}