using System;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace Aggrex.Common.BitSharp
{
    public class UInt128 : IComparable<UInt128>
    {
        public static UInt128 Zero { get; } = new UInt128(new byte[0]);
        public static UInt128 One { get; } = (UInt128)1;

        // parts are big-endian
        private const int width = 2;
        private readonly ulong[] parts;
        private readonly int hashCode;

        public UInt128()
        {
        }

        public UInt128(byte[] value)
        {
            if (value.Length > 16 && !(value.Length == 17 && value[16] == 0))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (value.Length < 16)
                Array.Resize(ref value, 16);

            InnerInit(value, 0, out parts);
        }

        private void InnerInit(byte[] buffer, int offset, out ulong[] parts)
        {
            // convert parts and store
            parts = new ulong[width];
            offset += 16;
            for (var i = 0; i < width; i++)
            {
                offset -= 8;
                parts[i] = Bits.ToUInt64(buffer, offset);
            }
        }

        public UInt128(int value)
            : this(Bits.GetBytes(value))
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
        }

        public UInt128(long value)
            : this(Bits.GetBytes(value))
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
        }

        public UInt128(uint value)
            : this(Bits.GetBytes(value))
        { }

        public UInt128(ulong value)
            : this(Bits.GetBytes(value))
        { }

        public UInt128(BigInteger value)
            : this(value.ToByteArray())
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
        }

        public ulong[] Parts => new ulong[] { parts[3], parts[2]};
        public ulong Part1 => parts[0];
        public ulong Part2 => parts[1];
        public byte[] ToByteArray()
        {
            var buffer = new byte[16];
            ToByteArray(buffer);
            return buffer;
        }

        public void ToByteArray(byte[] buffer, int offset = 0)
        {
            for (var i = width - 1; i >= 0; i--)
            {
                Bits.EncodeUInt64(parts[i], buffer, offset);
                offset += 8;
            }
        }

        public byte[] ToByteArrayBE()
        {
            var buffer = new byte[16];
            ToByteArrayBE(buffer);
            return buffer;
        }

        public void ToByteArrayBE(byte[] buffer, int offset = 0)
        {
            for (var i = 0; i < width; i++)
            {
                Bits.EncodeUInt64BE(parts[i], buffer, offset);
                offset += 8;
            }
        }

        public BigInteger ToBigInteger()
        {
            // add a trailing zero so that value is always positive
            var buffer = new byte[17];
            ToByteArray(buffer);
            return new BigInteger(buffer);
        }

        public int CompareTo(UInt128 other)
        {
            for (var i = 0; i < width; i++)
            {
                if (parts[i] < other.parts[i])
                    return -1;
                else if (parts[i] > other.parts[i])
                    return +1;
            }

            return 0;
        }

        // TODO doesn't compare against other numerics
        public override bool Equals(object obj)
        {
            if (!(obj is UInt128))
                return false;

            var other = (UInt128)obj;
            return this == other;
        }

        public override int GetHashCode() => hashCode;

        public override string ToString()
        {
            return this.ToHexNumberString();
        }

        public static explicit operator BigInteger(UInt128 value)
        {
            return value.ToBigInteger();
        }

        public static explicit operator UInt128(byte value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(int value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(long value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(sbyte value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(short value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(uint value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(ulong value)
        {
            return new UInt128(value);
        }

        public static explicit operator UInt128(ushort value)
        {
            return new UInt128(value);
        }

        public static bool operator ==(UInt128 left, UInt128 right)
        {
            if (ReferenceEquals(left, right))
                return true;
            else if (ReferenceEquals(left, null) != ReferenceEquals(right, null))
                return false;

            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(UInt128 left, UInt128 right)
        {
            return !(left == right);
        }

        public static bool operator <(UInt128 left, UInt128 right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(UInt128 left, UInt128 right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(UInt128 left, UInt128 right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(UInt128 left, UInt128 right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static UInt128 Parse(string value)
        {
            return new UInt128(BigInteger.Parse("0" + value).ToByteArray());
        }

        public static UInt128 Parse(string value, IFormatProvider provider)
        {
            return new UInt128(BigInteger.Parse("0" + value, provider).ToByteArray());
        }

        public static UInt128 Parse(string value, NumberStyles style)
        {
            return new UInt128(BigInteger.Parse("0" + value, style).ToByteArray());
        }

        public static UInt128 Parse(string value, NumberStyles style, IFormatProvider provider)
        {
            return new UInt128(BigInteger.Parse("0" + value, style, provider).ToByteArray());
        }

        public static UInt128 ParseHex(string value)
        {
            return new UInt128(BigInteger.Parse("0" + value, NumberStyles.HexNumber).ToByteArray());
        }

        public static UInt128 operator &(UInt128 left, UInt128 right)
        {
            return new UInt128(left.ToBigInteger() & right.ToBigInteger());
        }

        public static UInt128 operator +(UInt128 left, UInt128 right)
        {
            return new UInt128(left.ToBigInteger() + right.ToBigInteger());
        }

        public static UInt128 operator -(UInt128 left, UInt128 right)
        {
            return new UInt128(left.ToBigInteger() - right.ToBigInteger());
        }

        public static UInt128 operator *(UInt128 left, uint right)
        {
            return new UInt128(left.ToBigInteger() * right);
        }

        public static UInt128 operator /(UInt128 dividend, uint divisor)
        {
            return new UInt128(dividend.ToBigInteger() / divisor);
        }

        public static UInt128 operator <<(UInt128 value, int shift)
        {
            return new UInt128(value.ToBigInteger() << shift);
        }

        public static UInt128 operator >>(UInt128 value, int shift)
        {
            return new UInt128(value.ToBigInteger() >> shift);
        }
    }
}