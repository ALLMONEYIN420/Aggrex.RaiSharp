using System.Net;

namespace Aggrex.Network
{
    /// <summary>
    /// Represents a single node in the network. The interface is purposely kept simple.
    /// There should be very little (if any) upfront configuration needed to use an implemenation
    /// of this interface. Instead, if any specific dependencies are needed, they should be injected
    /// into the implemenation.
    /// </summary>
    public interface ILocalNode
    {
        IPEndPoint LocalAddress { get; }
        void Start();
    }
}