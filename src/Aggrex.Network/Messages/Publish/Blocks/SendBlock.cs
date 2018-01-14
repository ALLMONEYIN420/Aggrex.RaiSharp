using System.IO;
using System.Numerics;
using Aggrex.Common.BitSharp;
using Aggrex.ConsensusProtocol.Security;
using Aggrex.Network.Security;

namespace Aggrex.Network.Messages.Publish.Blocks
{
    public class SendBlock : Block
    {
        public SendHashables Hashables { get; set; }

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