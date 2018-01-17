using System.Collections.Generic;
using Aggrex.Common.BitSharp;
using Blake2Sharp;

namespace Aggrex.Network.Security
{
    public class OpenHashables
    {
        public OpenHashables(UInt256 source, UInt256 account, UInt256 representative)
        {
            Source = source;
            Account = account;
            Representative = representative;
        }

        public UInt256 Source { get; set; }
        public UInt256 Account { get; set; }
        public UInt256 Representative { get; set; }

        private Dictionary<string, string> Data { get; set; }

        public UInt256 Hash()
        {
            var hasher = Blake2B.Create(new Blake2BConfig()
            {
                OutputSizeInBytes = 64
            });

            hasher.Init();
            hasher.Update(Source.ToByteArray());
            hasher.Update(Representative.ToByteArray());
            hasher.Update(Account.ToByteArray());

            return new UInt256(hasher.Finish());
        }
    }
}