using System.IO;
using System.Net;
using Aggrex.Common;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.Messages.Addresses
{
    public class PeerAddressesPayloadMessage : BaseMessage, IStreamable
    {
        public IPEndPoint[] ConnectedIpEndPoints { get; set; }
        public IPEndPoint[] NotConnectedIpEndPoints { get; set; }

        #region Serialization

        public override MessageType MessageType => MessageType.PeerAddressesPayload;

        protected override void WriteProperties(BinaryWriter writer)
        {
            writer.Write(ConnectedIpEndPoints?.Length ?? 0);

            if (ConnectedIpEndPoints != null)
            {
                foreach (IPEndPoint ip in ConnectedIpEndPoints)
                {
                    byte[] ipBytes = ip.Address.GetAddressBytes();
                    writer.Write(ipBytes.Length);
                    writer.Write(ipBytes);
                    writer.Write(ip.Port); ;
                }
            }

            writer.Write(NotConnectedIpEndPoints?.Length ?? 0);

            if (NotConnectedIpEndPoints != null)
            {
                foreach (IPEndPoint ip in NotConnectedIpEndPoints)
                {
                    byte[] ipBytes = ip.Address.GetAddressBytes();
                    writer.Write(ipBytes.Length);
                    writer.Write(ipBytes);
                    writer.Write(ip.Port);
                }
            }
        }

        protected override void ReadProperties(BinaryReader reader)
        {
            // Connected
            var connectedCount = reader.ReadInt32();

            ConnectedIpEndPoints = new IPEndPoint[connectedCount];

            for (int i = 0; i < connectedCount; i++)
            {
                int len = reader.ReadInt32();
                IPAddress ipa = new IPAddress(reader.ReadBytes(len));
                IPEndPoint ipe = new IPEndPoint(ipa, reader.ReadInt32());
                ConnectedIpEndPoints[i] = ipe;
            }

            // Not Connected
            var noteConnectedCount = reader.ReadUInt32();

            NotConnectedIpEndPoints = new IPEndPoint[noteConnectedCount];

            for (int i = 0; i < noteConnectedCount; i++)
            {
                int len = reader.ReadInt32();
                IPAddress ipa = new IPAddress(reader.ReadBytes(len));
                IPEndPoint ipe = new IPEndPoint(ipa, reader.ReadInt32());
                NotConnectedIpEndPoints[i] = ipe;
            }
        }

        #endregion
    }
}
