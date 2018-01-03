using System.Net;

namespace Aggrex.Network.Packets
{
    public interface IPacketBroadcaster
    {
        void BroadCastPacket(byte[] data, int length, IPEndPoint recipient);
    }
}