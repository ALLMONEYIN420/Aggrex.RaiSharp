using System;
using System.IO;
using Aggrex.ConsensusProtocol.Messages.Transaction;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;

namespace Aggrex.ConsensusProtocol.MessageProcessors.Transactions
{
    public class TransactionMessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(TransactionMessage message, IRemoteNode remoteNode)
        {
        }
        public void ProcessMessage(BinaryReader reader, IRemoteNode remoteNode)
        {
        }
    }
}