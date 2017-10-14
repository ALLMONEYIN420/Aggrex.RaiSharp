using System.IO;
using Aggrex.ConsensusProtocol.Transaction;
using Aggrex.ConsensusProtocol.TransactionProcessors;
using Aggrex.Network;
using Aggrex.Network.ObjectReader;
using Autofac.Features.Indexed;

namespace Aggrex.ConsensusProtocol.Transactions.Dispatcher
{
    public class TransactionDispatcher : ITransactionDispatcher 
    {
        private IObjectReader _reader;
        private IIndex<TransactionType, ITransactionProcessor> _transactionProcessors;

        public TransactionDispatcher(IObjectReader reader, IIndex<TransactionType, ITransactionProcessor> transactionProcessors)
        {
            _reader = reader;
            _transactionProcessors = transactionProcessors;
        }

        public void DispatchTransaction(BinaryReader reader, IRemoteNode remoteNode)
        {
            TransactionType type = (TransactionType)reader.ReadByte();
            ITransactionProcessor processor = _transactionProcessors[type];
            processor.ProcessTransaction(reader, remoteNode);
        }
    }
}