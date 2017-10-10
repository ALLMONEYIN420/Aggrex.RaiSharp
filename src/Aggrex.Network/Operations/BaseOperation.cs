using System.IO;
using Aggrex.Common;

namespace Aggrex.Network.Operations
{
    public abstract class BaseOperation : IStreamable
    {
        #region Serialization

        protected abstract void WriteProperties(BinaryWriter writer);
        protected abstract void ReadProperties(BinaryReader writer);

        public void WriteToStream(BinaryWriter writer)
        {
            WriteProperties(writer);
        }

        public void ReadFromStream(BinaryReader reader)
        {
            ReadProperties(reader);
        }

        #endregion
    }
}