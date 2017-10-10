using System;
using Aggrex.ConsensusProtocol.Messages.Transaction;
using Aggrex.ConsensusProtocol.Transaction;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;

namespace Aggrex.ConsensusProtocol.MessageProcessors.Transactions
{
    public class TransactionMessageProcessor : IMessageProcessor<TransactionMessage>
    {
        public void ProcessMessage(TransactionMessage message, IRemoteNode remoteNode)
        {
        }
    }
}