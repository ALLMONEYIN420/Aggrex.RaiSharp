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

        public PacketSender()
        {
            _client = new UdpClient();
        }

        public async Task SendPacket(IStreamable packet, IPEndPoint recipient)
        {
            using (MemoryStream memStream = new MemoryStream(new byte[152]))
            using (BinaryWriter writer = new BinaryWriter(memStream))
            {
                packet.WriteToStream(writer);
                var bytePacket = memStream.ToArray();
                await _client.SendAsync(bytePacket, bytePacket.Length, recipient);
            }
        }
    }
}