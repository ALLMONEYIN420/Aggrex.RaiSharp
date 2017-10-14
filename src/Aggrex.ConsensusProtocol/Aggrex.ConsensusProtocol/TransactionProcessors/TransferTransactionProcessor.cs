using System;
using System.IO;
using Aggrex.ConsensusProtocol.Transaction;
using Aggrex.Network;
using Aggrex.Network.ObjectReader;
using Autofac.Features.AttributeFilters;

namespace Aggrex.ConsensusProtocol.TransactionProcessors
{
    public class TransferTransactionProcessor : ITransactionProcessor
    {
        private IPeerTracker _peerTracker;
        private IObjectReader _objectReader;

        public TransferTransactionProcessor(
        IPeerTracker peerTracker,
        IObjectReader objectReader)
        {
            _peerTracker = peerTracker;
            _objectReader = objectReader;
        }

        public void ProcessTransaction(TransferTransaction transaction, IRemoteNode remoteNode)
        {
            Console.WriteLine("Received a transfer transaction!");
        }

        public void ProcessTransaction(BinaryReader reader, IRemoteNode remoteNode)
        {
            ProcessTransaction(_objectReader.ReadObject<TransferTransaction>(reader), remoteNode);
        }
    }
}