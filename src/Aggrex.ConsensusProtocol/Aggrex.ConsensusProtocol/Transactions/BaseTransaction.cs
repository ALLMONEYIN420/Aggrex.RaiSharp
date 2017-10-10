using System.IO;
using Aggrex.Common;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.Transaction
{
    public abstract class BaseTransaction : IStreamable
    {
        public abstract TransactionType TransactionType { get; }
        protected abstract void WriteProperties(BinaryWriter writer);
        protected abstract void ReadProperties(BinaryReader reader);

        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write((byte)TransactionType);
            WriteProperties(writer);
        }

        public void ReadFromStream(BinaryReader reader)
        {
            ReadProperties(reader);
        }
    }
}