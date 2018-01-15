using Aggrex.Common.BitSharp;
using LiteDB;

namespace Aggrex.Network.Models
{
    public class FrontiersDb
    {
        [BsonId]
        public UInt256 BlockHash { get; set; }
        public UInt256 Account { get; set; }
    }
}