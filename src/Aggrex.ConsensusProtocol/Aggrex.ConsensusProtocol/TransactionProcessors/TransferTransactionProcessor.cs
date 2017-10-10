using System;
using Aggrex.ConsensusProtocol.Transaction;
using Aggrex.Network;

namespace Aggrex.ConsensusProtocol.TransactionProcessors
{
    public class TransferTransactionProcessor : ITransactionProcessor<TransferTransaction>
    {
        private IPeerTracker _peerTracker;
        public TransferTransactionProcessor(IPeerTracker peerTracker)
        {
            _peerTracker = peerTracker;
        }

        public void ProcessTransaction(TransferTransaction transaction, IRemoteNode remoteNode)
        {
            Console.WriteLine("Received a transfer transaction!");
        }
    }
}