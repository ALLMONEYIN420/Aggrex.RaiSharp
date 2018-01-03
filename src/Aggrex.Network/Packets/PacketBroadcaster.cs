using System.Net;
using System.Net.Sockets;

namespace Aggrex.Network.Packets
{
    public class PacketBroadcaster : IPacketBroadcaster
    {
        private UdpClient _client;

        public PacketBroadcaster()
        {
           _client = new UdpClient(); 
        }

        public void BroadCastPacket(byte[] data, int length, IPEndPoint recipient)
        {
            _client.Send(data, data.Length, recipient);
        }
    }
}