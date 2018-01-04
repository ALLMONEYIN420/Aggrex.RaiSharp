using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.Network.Packets
{
    public class PacketSender : IPacketSender
    {
        private UdpClient _client;
        private MessageHeader _messageHeader;

        public PacketSender()
        {
            _client = new UdpClient();
            _messageHeader = new MessageHeader();
            _messageHeader.Extensions = new byte[2];
            _messageHeader.VersionMax = 5;
            _messageHeader.VersionMin = 1;
            _messageHeader.VersionUsing = 5;
            _messageHeader.Type = MessageType.Keepalive;
        }

        public async Task SendPacket(IStreamable packet, IPEndPoint recipient)
        {
            using (MemoryStream memStream = new MemoryStream(new byte[152]))
            using (BinaryWriter writer = new BinaryWriter(memStream))
            {
                _messageHeader.WriteToStream(writer);
                packet.WriteToStream(writer);
                var bytePacket = memStream.ToArray();
                await _client.SendAsync(bytePacket, bytePacket.Length, recipient);
            }
        }
    }
}