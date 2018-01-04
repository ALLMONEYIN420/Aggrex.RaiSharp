using System.Net;
using System.Threading.Tasks;
using Aggrex.Common;

namespace Aggrex.Network.Packets
{
    public interface IPacketSender
    {
        Task SendPacket(IStreamable data, IPEndPoint recipient);
    }
}