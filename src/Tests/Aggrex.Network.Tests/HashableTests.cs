using System;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using Aggrex.Common.BitSharp;
using Aggrex.ConsensusProtocol.Accounts;
using Aggrex.ConsensusProtocol.Security;
using Blake2Sharp;
using Xunit;

namespace Aggrex.Network.Tests
{
    public class HashableTests
    {
        [Fact]
        public void ThrowIfAmountTooBig()
        {
            UInt256 destination = UInt256.Zero;
            UInt256 previous = UInt256.Zero;

            byte[] amountToBig = new byte[18];
            for (int i = 0; i < amountToBig.Length - 1; i++)
            {
                amountToBig[i] = 0xff;
            }
            amountToBig[amountToBig.Length - 1] = 0x00;

            BigInteger amount = new BigInteger(amountToBig);
            Assert.Throws<InvalidDataException>(() =>
            {
                var sendHashables = new SendHashables(destination, previous, amount);
            });
        }
    }
}