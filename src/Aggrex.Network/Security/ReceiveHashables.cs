using System.Collections.Generic;
using Aggrex.Common.BitSharp;
using Blake2Sharp;

namespace Aggrex.Network.Security
{
    public class ReceiveHashables
    {
        public ReceiveHashables(UInt256 previous, UInt256 source)
        {
            Previous = previous;
            Source = source;
        }

        public UInt256 Previous { get; set; }

        public UInt256 Source { get; set; }

        public UInt256 Hash()
        {
            var hasher = Blake2B.Create(new Blake2BConfig()
            {
                OutputSizeInBytes = 64
            });

            hasher.Init();
            hasher.Update(Previous.ToByteArray());
            hasher.Update(Source.ToByteArray());

            return new UInt256(hasher.Finish());
        }
    }
}