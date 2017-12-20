using System.IO;
using Aggrex.Common;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    public abstract class BaseMessage : IStreamable
    {
        public MessageHeader MessageHeader { get; set; }
        protected abstract void WriteProperties(BinaryWriter writer);
        protected abstract void ReadProperties(BinaryReader reader);

        public void WriteToStream(BinaryWriter writer)
        {
            MessageHeader.WriteToStream(writer);
            WriteProperties(writer);
        }

        public void ReadFromStream(BinaryReader reader)
        {
            MessageHeader.ReadFromStream(reader);
            ReadProperties(reader);
        }
    }
}