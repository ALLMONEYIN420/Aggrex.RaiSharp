using Aggrex.Common.BitSharp;

namespace Aggrex.Database.Models
{
    public class AccountsDb
    {
        public UInt256 BlockHash { get; set; }
        public UInt256 Account { get; set; }
    }
}