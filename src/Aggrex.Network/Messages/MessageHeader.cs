using System.IO;
using System.Linq;
using Aggrex.Common;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    public class MessageHeader : IStreamable
    {
        public static byte[] MagicNumber { get; } = new[] { (byte)'R', (byte)'C' };
        public sbyte VersionMax { get; set; }
        public sbyte VersionMin { get; set; }
        public sbyte VersionUsing { get; set; }
        public MessageType Type { get; set; }
        public byte[] Extensions { get; set; }

        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write(MagicNumber);
            writer.Write(VersionMax);
            writer.Write(VersionUsing);
            writer.Write(VersionMin);
            writer.Write((byte)Type);
            writer.Write(Extensions);
        }

        public bool ReadFromStream(BinaryReader reader)
        {
            try
            {
                var magicNumber = reader.ReadBytes(MagicNumber.Length);
                if (!Enumerable.SequenceEqual(magicNumber, MagicNumber))
                {
                    return false;
                }

                VersionMax = (sbyte) reader.ReadByte();
                VersionUsing = (sbyte) reader.ReadByte();
                VersionMin = (sbyte) reader.ReadByte();
                Type = (MessageType) reader.ReadByte();
                Extensions = reader.ReadBytes(2);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
