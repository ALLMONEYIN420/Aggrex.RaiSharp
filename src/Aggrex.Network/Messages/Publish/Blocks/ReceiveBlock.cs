using System.IO;

namespace Aggrex.Network.Messages.Publish.Blocks
{
    public class ReceiveBlock : Block
    {
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