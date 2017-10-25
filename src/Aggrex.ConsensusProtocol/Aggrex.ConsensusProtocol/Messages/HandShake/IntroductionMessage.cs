using System;
using System.IO;
using Aggrex.Common;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.Messages.HandShake
{
    public class IntroductionMessage : BaseMessage, IStreamable
    {
        public Version Version { get; set; }
        public ulong BlockHeight { get; set; }
        public string DNID { get; set; }
        public int Port { get; set; }

        #region Serialization

        public override MessageType MessageType => MessageType.Introduction;

        protected override void WriteProperties(BinaryWriter writer)
        {
            writer.Write(Version.ToString(2));
            writer.Write(BlockHeight);
            writer.Write(Port);
            writer.Write(DNID);
        }

        protected override void ReadProperties(BinaryReader reader)
        {
            Version = Version.Parse(reader.ReadString());
            BlockHeight = reader.ReadUInt64();
            Port = reader.ReadInt32();
            DNID = reader.ReadString();
        }

        #endregion

    }
}
