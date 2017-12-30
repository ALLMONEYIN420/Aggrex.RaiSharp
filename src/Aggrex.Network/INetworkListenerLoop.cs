using System;
using System.Net;
using System.Net.Sockets;

namespace Aggrex.Network
{
    /// <summary>
    /// Used to listen for network connections.
    /// </summary>
    public interface INetworkListenerLoop
    {
        event EventHandler<TcpClient> TcpConnectionEstablished;

        event EventHandler<byte[]> UdpPacketReceived;
        void ExecuteTcpListenerLoop();
        void ExecuteUdpListenerLoop();
    }
}