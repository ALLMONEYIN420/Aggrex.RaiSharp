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

        event EventHandler<DataGramReceivedArgs> DatagramReceived;

        void ExecuteTcpListenerLoop();
        void ExecuteUdpListenerLoop();
    }


    public struct DataGramReceivedArgs
    {
        public byte[] Data { get; set; }
        public IPEndPoint Sender { get; set; }
    }
}