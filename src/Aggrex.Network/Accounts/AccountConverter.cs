using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Aggrex.Common.BitSharp;
using Blake2Sharp;

namespace Aggrex.Network.Accounts
{
    public class AccountConverter : IAccountConverter
    {
        private static string AccountReverse = "~0~1234567~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~89:;<=>?@AB~CDEFGHIJK~LMNO~~~~~";

        private static string AccountLookup = "13456789abcdefghijkmnopqrstuwxyz";

        public UInt256 DecodeAccount(string account)
        {
            if (account.Length != 64)
            {
                throw new InvalidDataException("length should be 64");
            }

            if (!account.StartsWith("xrb_"))
            {
                throw new InvalidDataException("invalid account format");
            }

            BigInteger number = new BigInteger();

            for (int i = 4; i < account.Length; i++)
            {
                sbyte byteResult = DecodeChar(account[i]);
                if (byteResult != '~')
                {
                    number <<= 5;
                    number += byteResult;
                }
            }

            UInt256 accountUInt256 = new UInt256(number >> 40);
            var accountBytes = accountUInt256.ToByteArray().Reverse().ToArray();
            UInt64 check = BitConverter.ToUInt64(number.ToByteArray(), 0);
            check &= 0xffffffffff;
            var hasher = Blake2B.Create(new Blake2BConfig { OutputSizeInBytes = 5 });
            hasher.Update(accountBytes);
            var final = hasher.Finish();
            Array.Resize(ref final, 8);
            UInt64 validation = BitConverter.ToUInt64(final, 0);

            if (check != validation)
            {
                throw new InvalidDataException();
            }

            return accountUInt256;
        }
       
        public string EncodeAccount(UInt256 account)
        {
            var hasher = Blake2B.Create(new Blake2BConfig
            {
                OutputSizeInBytes = 5
            });

            var bytes = account.ToByteArray().Reverse().ToArray();
            hasher.Update(bytes);
            byte[] checkBytes = hasher.Finish();
            Array.Resize(ref checkBytes, 8);
            UInt64 check = BitConverter.ToUInt64(checkBytes, 0);
            BigInteger number = Number(bytes);
            number <<= 40;
            number |= new BigInteger(check);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 60; i++)
            {
                var r = number.ToByteArray()[0] & 0x1f;
                number >>= 5;
                sb.Append(AccountLookup[r]);
            }
            sb.Append("_brx");
            var addrReverse = sb.ToString();
            var arr = addrReverse.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private BigInteger Number(byte[] bytes)
        {
            BigInteger result = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                result <<= 8;
                result |= bytes[i];
            }

            return result;
        }

        private sbyte DecodeChar(char c)
        {
            if (c < '0' || c > '~')
            {
                throw new InvalidDataException("invalid account format");
            }

            return (sbyte)(AccountReverse[c - 0x30] - 0x30);
        }
    }
}