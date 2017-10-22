using System.IO;
using Aggrex.Common;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.Messages.Addresses
{
    public class RequestPeerAddressesMessage : BaseMessage, IStreamable
    {
        public override MessageType MessageType => MessageType.GetPeerAddresses;

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
