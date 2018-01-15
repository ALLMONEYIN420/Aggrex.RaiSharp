using Aggrex.Common.BitSharp;
using Aggrex.Common.ThrowHelpers.Exceptions;
using Aggrex.Common.ThrowHelpers.ExceptionTypes;

namespace Aggrex.Common.ThrowHelpers
{
    public static class ThrowHelper
    {
        public static class Sync
        {
            public static void ThrowIfGapDetected(UInt256 headBlock, UInt256 previous)
            {
                if (headBlock != previous)
                {
                    throw new BlockGapException();
                }
            }
        }


        public static class BadActor
        {
            public static void ThrowIfOverspendDetected(UInt128 headBalance, UInt128 blockBalance)
            {
                if (headBalance <= blockBalance)
                {
                    throw new BlockGapException();
                }
            }
        }

        public static class Sanity
        {
            public static void ThrowIfZero(UInt128 value)
            {
                if (value == UInt128.Zero)
                {
                    throw new SanityCheckException();
                }
            }

            public static void ThrowIfTrue(bool value)
            {
                if (value)
                {
                    throw new SanityCheckException();
                }
            }

            public static void ThrowIfFalse(bool value)
            {
                if (!value)
                {
                    throw new SanityCheckException();
                }
            }

            public static void ThrowIfNull(object value)
            {
                if (value == null)
                {
                    throw new SanityCheckException();
                }
            }

            public static void ThrowIfNotNull(object value)
            {
                if (value != null)
                {
                    throw new SanityCheckException();
                }
            }
        }





    }

}