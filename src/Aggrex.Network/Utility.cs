using System.Numerics;
using Aggrex.Common.BitSharp;
using Chaos.NaCl;

namespace Aggrex.Network
{
    public static class Utility
    {
        public static bool ValidateMessage(UInt256 account, UInt256 message, BigInteger signature)
        {
            return Ed25519.Verify(signature.ToByteArray(), message.ToByteArray(), account.ToByteArray());
        }
    }
}