using System.IO;
using Aggrex.Common.BitSharp;
using Aggrex.Network.Security;

namespace Aggrex.Network.Messages.Publish.Blocks
{
    public class OpenBlock : Block
    {
        public OpenHashables Hashables { get; set; }
        public override UInt256 Representative => Hashables.Representative;
        public override UInt256 Previous { get; } = UInt256.Zero;

        public override UInt256 Hash()
        {
            return Hashables.Hash();
        }

        protected override void WriteProperties(BinaryWriter writer)
        {
        }

        protected override bool ReadProperties(BinaryReader reader)
        {
            return true;
        }

        public override void Accept(IBlockVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}