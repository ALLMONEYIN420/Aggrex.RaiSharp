using System.IO;
using System.Numerics;
using Aggrex.Common.BitSharp;
using Aggrex.Network.Security;

namespace Aggrex.Network.Messages.Publish.Blocks
{
    public class SendBlock : Block
    {
        public SendHashables Hashables { get; set; }
        public override UInt256 Representative { get; } = UInt256.Zero;
        public override UInt256 Previous => Hashables.Previous;

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