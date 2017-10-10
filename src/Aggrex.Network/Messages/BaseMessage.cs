using System.IO;
using Aggrex.Common;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    public abstract class BaseMessage : IStreamable
    {
        public abstract MessageType MessageType { get; }
        protected abstract void WriteProperties(BinaryWriter writer);
        protected abstract void ReadProperties(BinaryReader reader);

        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write((byte)MessageType);
            WriteProperties(writer);
        }

        public void ReadFromStream(BinaryReader reader)
        {
            ReadProperties(reader);
        }
    }
}