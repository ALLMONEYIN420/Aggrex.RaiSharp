using System.Collections.Generic;
using System.IO;
using System.Net;
using Aggrex.Common;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;
using Newtonsoft.Json;

namespace Aggrex.ConsensusProtocol.Messages.ActiveNodeSet
{
    public class ActiveNodeSetPayloadMessage : BaseMessage, IStreamable
    {
        public IDictionary<string, int> Confirmations { get; set; }

        #region Serialization

        public override MessageType MessageType => MessageType.ActiveNodeSetPayload;

        protected override void WriteProperties(BinaryWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(Confirmations, Formatting.None));
        }

        protected override void ReadProperties(BinaryReader reader)
        {
            Confirmations = JsonConvert.DeserializeObject<Dictionary<string, int>>(reader.ReadString());
        }

        #endregion
    }
}
