using System.Collections.Generic;
using Aggrex.Common.BitSharp;
using Aggrex.Framework.Security;
using Blake2Sharp;

namespace Aggrex.Network.Security
{
    public class OpenHashables : IHashable
    {
        public OpenHashables(UInt256 source, UInt256 account, UInt256 representative)
        {
            Source = source;
            Account = account;
            Representative = representative;
        }

        private UInt256 Source { get; set; }
        private UInt256 Account { get; set; }
        private UInt256 Representative { get; set; }

        private Dictionary<string, string> Data { get; set; }

        public void Hash(Blake2BConfig config, byte[] message)
        {
            var hasher = Blake2B.Create(new Blake2BConfig()
            {
                OutputSizeInBytes = 64
            });

            hasher.Init();
            hasher.Update(Source.ToByteArray());
            hasher.Update(Representative.ToByteArray());
            hasher.Update(Account.ToByteArray());
        }
    }
}