using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Aggrex.Common.BitSharp;
using Aggrex.Framework.Security;
using Blake2Sharp;

namespace Aggrex.ConsensusProtocol.Security
{
    public class SendHashables : IHashable
    {
        private static BigInteger MaxSendAmount = new BigInteger(new byte[]{0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00 });

        public SendHashables(UInt256 destination, UInt256 previous, BigInteger balance)
        {
            if (balance > MaxSendAmount)
            {
                throw new InvalidDataException("amount too big.");
            }

            Destination = destination;
            Previous = previous;
            Balance = balance;
        }

        private UInt256 Destination { get; set; }
        private UInt256 Previous { get; set; }
        private BigInteger Balance { get; set; }

        private Dictionary<string, string> Data { get; set; }

        public void Hash(Blake2BConfig config, byte[] message)
        {
            var hasher = Blake2B.Create(new Blake2BConfig()
            {
                OutputSizeInBytes = 64
            });

            hasher.Init();
            hasher.Update(Previous.ToByteArray());
            hasher.Update(Destination.ToByteArray());
            hasher.Update(Balance.ToByteArray());
        }
    }
}