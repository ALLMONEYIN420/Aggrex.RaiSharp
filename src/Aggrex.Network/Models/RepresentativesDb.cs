using Aggrex.Common.BitSharp;
using LiteDB;

namespace Aggrex.Network.Models
{
    public class RepresentativesDb
    {
        [BsonId]
        public UInt256 Account { get; set; }
        public UInt128 Weight { get; set; }
    }
}