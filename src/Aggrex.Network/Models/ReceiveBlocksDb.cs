using Aggrex.Common.BitSharp;
using Aggrex.Network.Messages.Publish.Blocks;
using LiteDB;

namespace Aggrex.Network.Models
{
    public class ReceiveBlocksDb
    {
        [BsonId]
        public UInt256 BlockHash { get; set; }
        public ReceiveBlock ReceiveBlock { get; set; }
    }
}