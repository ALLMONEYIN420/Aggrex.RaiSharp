using System.IO;
using System.Net;
using Aggrex.Network;
using Aggrex.Network.Messages;
using Aggrex.Network.ObjectReader;
using Aggrex.Network.Requests;
using Autofac;
using Autofac.Features.Indexed;

namespace Aggrex.ConsensusProtocol.Messages
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
            }
        }
    }
}