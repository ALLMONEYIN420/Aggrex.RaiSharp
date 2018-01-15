using System.IO;
using Aggrex.Common.BitSharp;
using Aggrex.Network.Security;

namespace Aggrex.Network.Messages.Publish.Blocks
{
    public class ChangeBlock : Block
    {
        public ChangeHashables Hashables { get; set; }
        public override UInt256 Representative => Hashables.Representative;
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