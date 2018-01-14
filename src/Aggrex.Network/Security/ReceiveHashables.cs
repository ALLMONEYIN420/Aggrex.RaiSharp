using System.Collections.Generic;
using Aggrex.Common.BitSharp;
using Aggrex.Framework.Security;
using Blake2Sharp;

namespace Aggrex.Network.Security
{
    public class ReceiveHashables : IHashable
    {
        public ReceiveHashables(UInt256 previous, UInt256 source)
        {
            Previous = previous;
            Source = source;
        }

        private UInt256 Previous { get; set; }
        private UInt256 Source { get; set; }

        private Dictionary<string, string> Data { get; set; }

        public void Hash(Blake2BConfig config, byte[] message)
        {
            var hasher = Blake2B.Create(new Blake2BConfig()
            {
                OutputSizeInBytes = 64
            });

            hasher.Init();
            hasher.Update(Previous.ToByteArray());
            hasher.Update(Source.ToByteArray());
        }
    }
}