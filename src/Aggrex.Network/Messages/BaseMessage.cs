using System.IO;
using Aggrex.Common;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    public abstract class BaseMessage : IStreamable
    {
        protected abstract void WriteProperties(BinaryWriter writer);
        protected abstract bool ReadProperties(BinaryReader reader);

        public void WriteToStream(BinaryWriter writer)
        {
            WriteProperties(writer);
        }

        public bool ReadFromStream(BinaryReader reader)
        {
            return ReadProperties(reader);
        }
    }
}