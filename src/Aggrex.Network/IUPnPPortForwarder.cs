using System.Net;
using System.Threading.Tasks;

namespace Aggrex.Network
{
    public interface IUPnPPortForwarder
    {
        Task ForwardPortIfNatFound();
    }
}