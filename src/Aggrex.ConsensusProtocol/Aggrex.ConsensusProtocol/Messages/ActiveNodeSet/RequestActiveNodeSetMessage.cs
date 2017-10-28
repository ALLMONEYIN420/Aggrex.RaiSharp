using System.IO;
using Aggrex.Common;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.Messages.ActiveNodeSet
{
    public class RequestActiveNodeSetMessage : BaseMessage, IStreamable
    {
        public override MessageType MessageType => MessageType.RequestActiveNodeSet;

        #region Serialization

        protected override void WriteProperties(BinaryWriter writer)
        {
        }

        protected override void ReadProperties(BinaryReader reader)
        {
        }

        #endregion
    }
}
