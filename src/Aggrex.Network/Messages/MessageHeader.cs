using System.IO;
using Aggrex.Common;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    public class MessageHeader : IStreamable
    {
        public static byte[] MagicNumber { get; } = new[] {(byte)'R', (byte)'C' };
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

        public void ReadFromStream(BinaryReader reader)
        {
            reader.ReadBytes(MagicNumber.Length);
            VersionMax = (sbyte)reader.ReadByte();
            VersionUsing = (sbyte)reader.ReadByte();
            VersionMin = (sbyte)reader.ReadByte();
            Type = (MessageType)reader.ReadByte();
            Extensions = reader.ReadBytes(2);
        }
    }
}
