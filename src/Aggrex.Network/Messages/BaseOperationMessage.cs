using System.IO;
using Aggrex.Common;
using Aggrex.Network.Operations;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Messages
{
    public class BaseOperationMessage<T> : BaseMessage, IStreamable where T : BaseOperation, new()
    {
        public T Operation { get; set; } = new T();

        #region Serialization

        public override MessageType MessageType => MessageType.Operation;

        protected override void WriteProperties(BinaryWriter writer)
        {
            Operation.WriteToStream(writer);
        }

        protected override void ReadProperties(BinaryReader reader)
        {
            Operation.ReadFromStream(reader);
        }

        #endregion
    }
}
