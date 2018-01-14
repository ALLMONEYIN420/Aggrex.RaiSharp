using System.Collections.Generic;
using System.Numerics;
using Aggrex.Common.BitSharp;
using Aggrex.Framework.Security;
using Blake2Sharp;

namespace Aggrex.ConsensusProtocol.Security
{
    public class ChangeHashables : IHashable
    {
        public ChangeHashables(UInt256 previous, UInt256 representative)
        {
            Previous = previous;
            Representative = representative;
        }

        private UInt256 Previous { get; set; }
        private UInt256 Representative { get; set; }
        private Dictionary<string, string> Data { get; set; }
        public void Hash(Blake2BConfig config, byte[] message)
        {
            var hasher = Blake2B.Create(new Blake2BConfig()
            {
                OutputSizeInBytes = 64
            });

            hasher.Init();
            hasher.Update(Previous.ToByteArray());
            hasher.Update(Representative.ToByteArray());
        }
    }
}