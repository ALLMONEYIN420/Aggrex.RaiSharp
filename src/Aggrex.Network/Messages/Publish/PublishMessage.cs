using System;
using System.IO;
using System.Linq;
using System.Net;
using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network.Messages.Publish
{
    public class PublishMessage : BaseMessage
    {
        public Block Block { get; set; }

        public PublishMessage(MessageHeader header) : base(header)
        {
        }

        public IPEndPoint[] Peers { get; set; } = new IPEndPoint[8];

        protected override void WriteProperties(BinaryWriter writer)
        {
        }

        protected override bool ReadProperties(BinaryReader reader)
        {
            BlockType blockType = (BlockType)((BitConverter.ToUInt16(Header.Extensions, 0) & Block.BlockTypeMask) >> 8);
            switch (blockType)
            {
                case BlockType.Receive:
                    Block = new ReceiveBlock();
                    break;
                case BlockType.Send:
                    Block = new SendBlock();
                    break;
                case BlockType.Open:
                    Block = new OpenBlock();
                    break;
                case BlockType.Change:
                    Block = new ChangeBlock();
                    break;
            }

            Block.ReadFromStream(reader);
            return true;
        }
    }
}