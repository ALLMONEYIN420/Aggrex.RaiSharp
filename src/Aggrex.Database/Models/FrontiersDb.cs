using Aggrex.Common.BitSharp;
using LiteDB;

namespace Aggrex.Database.Models
{
    public class FrontiersDb
    {
        [BsonId]
        public UInt256 Account { get; set; }
        public byte[] ValueBytes { get; set; }
    }
}