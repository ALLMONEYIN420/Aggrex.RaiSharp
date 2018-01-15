using System;
using Aggrex.Common.BitSharp;
using LiteDB;

namespace Aggrex.Network.Models
{
    public class LatestAccountInfoDb
    {
        [BsonId]
        public UInt256 Account { get; set; }
        public UInt256 Head { get; set; }
        public UInt256 RepBlock { get; set; }
        public UInt256 OpenBlock { get; set; }
        public UInt128 Balance { get; set; }
        public UInt64 Modified { get; set; }
        public UInt64 BlockCount { get; set; }
    }
}