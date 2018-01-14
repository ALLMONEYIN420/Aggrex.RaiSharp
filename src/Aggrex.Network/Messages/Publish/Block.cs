using System;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ComTypes;
using Aggrex.Common;
using Aggrex.Common.BitSharp;
using Blake2Sharp;

namespace Aggrex.Network.Messages.Publish
{
    public abstract class Block : IStreamable, IBlockAcceptor
    {
        public static UInt32 BlockTypeMask = 0x0F00;
        public BigInteger Signature { get; set; }
        public UInt64 Work { get; set; }
        public UInt256 Hash()
        {
            return null;

        }

        public bool ValidateMessage()
        {
            return true;
        }

        protected abstract void WriteProperties(BinaryWriter writer);

        protected abstract bool ReadProperties(BinaryReader reader);

        public BlockType BlockType { get; set; }

        public void WriteToStream(BinaryWriter writer)
        {
        }

        public bool ReadFromStream(BinaryReader reader)
        {
            Signature = new BigInteger(reader.ReadBytes(64));
            Work = reader.ReadUInt64();
            return ReadProperties(reader);
        }

        public abstract void Accept(IBlockVisitor visitor);
    }
}