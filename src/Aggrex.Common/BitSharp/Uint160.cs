using System;
using System.Globalization;
using System.Numerics;

namespace Aggrex.Common.BitSharp
{
    public class UInt160 : IComparable<UInt160>
    {
        public static UInt160 Zero { get; } = new UInt160(new byte[0]);
        public static UInt160 One { get; } = (UInt160)1;

        // parts are big-endian
        private const int width = 5;
        private readonly uint[] parts;

        public UInt160(byte[] value)
        {
            // length must be <= 32, or 33 with the last byte set to 0 to indicate the number is positive
            if (value.Length > 20 && !(value.Length == 21 && value[20] == 0))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (value.Length < 20)
                Array.Resize(ref value, 20);

            InnerInit(value, 0, out parts);
        }

        private void InnerInit(byte[] buffer, int offset, out uint[] parts)
        {
            // convert parts and store
            parts = new uint[width];
            offset += 20;
            for (var i = 0; i < width; i++)
            {
                offset -= 4;
                parts[i] = Bits.ToUInt32(buffer, offset);
            }
        }

        public UInt160(int value)
            : this(Bits.GetBytes(value))
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
        }

        public UInt160(long value)
            : this(Bits.GetBytes(value))
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
        }

        public UInt160(uint value)
            : this(Bits.GetBytes(value))
        { }

        public UInt160(ulong value)
            : this(Bits.GetBytes(value))
        { }

        public UInt160(BigInteger value)
            : this(value.ToByteArray())
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
        }

        public uint[] Parts => new uint[] {parts[4], parts[3], parts[2], parts[1], parts[0] };
        public uint Part1 => parts[0];
        public uint Part2 => parts[1];
        public uint Part3 => parts[2];
        public uint Part4 => parts[3];

        public byte[] ToByteArray()
        {
            var buffer = new byte[20];
            ToByteArray(buffer);
            return buffer;
        }

        public void ToByteArray(byte[] buffer, int offset = 0)
        {
            for (var i = width - 1; i >= 0; i--)
            {
                Bits.EncodeUInt64(parts[i], buffer, offset);
                offset += 4;
            }
        }

        public byte[] ToByteArrayBE()
        {
            var buffer = new byte[20];
            ToByteArrayBE(buffer);
            return buffer;
        }

        public void ToByteArrayBE(byte[] buffer, int offset = 0)
        {
            for (var i = 0; i < width; i++)
            {
                Bits.EncodeUInt32BE(parts[i], buffer, offset);
                offset += 4;
            }
        }

        public BigInteger ToBigInteger()
        {
            // add a trailing zero so that value is always positive
            var buffer = new byte[21];
            ToByteArray(buffer);
            return new BigInteger(buffer);
        }

        public int CompareTo(UInt160 other)
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
            if (!(obj is UInt160))
                return false;

            var other = (UInt160)obj;
            return this == other;
        }


        public override string ToString()
        {
            return this.ToHexNumberString();
        }

        public static explicit operator BigInteger(UInt160 value)
        {
            return value.ToBigInteger();
        }

        public static explicit operator UInt160(byte value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(int value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(long value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(sbyte value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(short value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(uint value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(ulong value)
        {
            return new UInt160(value);
        }

        public static explicit operator UInt160(ushort value)
        {
            return new UInt160(value);
        }

        public static bool operator ==(UInt160 left, UInt160 right)
        {
            if (ReferenceEquals(left, right))
                return true;
            else if (ReferenceEquals(left, null) != ReferenceEquals(right, null))
                return false;

            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(UInt160 left, UInt160 right)
        {
            return !(left == right);
        }

        public static bool operator <(UInt160 left, UInt160 right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(UInt160 left, UInt160 right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(UInt160 left, UInt160 right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(UInt160 left, UInt160 right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static UInt160 Parse(string value)
        {
            return new UInt160(BigInteger.Parse("0" + value).ToByteArray());
        }

        public static UInt160 Parse(string value, IFormatProvider provider)
        {
            return new UInt160(BigInteger.Parse("0" + value, provider).ToByteArray());
        }

        public static UInt160 Parse(string value, NumberStyles style)
        {
            return new UInt160(BigInteger.Parse("0" + value, style).ToByteArray());
        }

        public static UInt160 Parse(string value, NumberStyles style, IFormatProvider provider)
        {
            return new UInt160(BigInteger.Parse("0" + value, style, provider).ToByteArray());
        }

        public static UInt160 ParseHex(string value)
        {
            return new UInt160(BigInteger.Parse("0" + value, NumberStyles.HexNumber).ToByteArray());
        }

        public static UInt160 operator +(UInt160 left, UInt160 right)
        {
            return new UInt160(left.ToBigInteger() + right.ToBigInteger());
        }

        public static UInt160 operator -(UInt160 left, UInt160 right)
        {
            return new UInt160(left.ToBigInteger() - right.ToBigInteger());
        }

        public static UInt160 operator *(UInt160 left, uint right)
        {
            return new UInt160(left.ToBigInteger() * right);
        }

        public static UInt160 operator /(UInt160 dividend, uint divisor)
        {
            return new UInt160(dividend.ToBigInteger() / divisor);
        }

        public static UInt160 operator <<(UInt160 value, int shift)
        {
            return new UInt160(value.ToBigInteger() << shift);
        }

        public static UInt160 operator >>(UInt160 value, int shift)
        {
            return new UInt160(value.ToBigInteger() >> shift);
        }
    }
}