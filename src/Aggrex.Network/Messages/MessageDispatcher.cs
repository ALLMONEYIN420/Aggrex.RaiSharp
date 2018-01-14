using System.IO;
using System.Net;
using Aggrex.Network.Requests;
using Autofac.Features.Indexed;

namespace Aggrex.Network.Messages
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IIndex<MessageType, IMessageProcessor> _messageProcessors;

        public MessageDispatcher(IIndex<MessageType, IMessageProcessor> messageProcessors)
        {
            _messageProcessors = messageProcessors;
        }

        public void DispatchDatagramMessage(MessageHeader messageHeader, BinaryReader reader, IPEndPoint sender)
        {
            switch (messageHeader.Type)
            {
                case MessageType.Keepalive:
                    _messageProcessors[MessageType.Keepalive].ProcessUdpMessage(messageHeader, reader, sender);
                    break;
                case MessageType.Publish:
                    _messageProcessors[MessageType.Publish].ProcessUdpMessage(messageHeader, reader, sender);
                    break;
            }
        }
    }
}