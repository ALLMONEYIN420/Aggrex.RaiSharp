using System.IO;
using System.Net;

namespace Aggrex.Network.Messages.KeepAlive
{
    public class KeepAliveMessageProcessor : IMessageProcessor
    {
        public void ProcessUdpMessage(MessageHeader messageHeader, BinaryReader reader, IPEndPoint sender)
        {
            KeepAliveMessage msg = new KeepAliveMessage();
            if (msg.ReadFromStream(reader))
            {

            }
        }
    }
}