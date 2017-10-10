using System.IO;
using Aggrex.Network;
using Aggrex.Network.Messages.MessageProcessor;
using Aggrex.Network.ObjectReader;
using Aggrex.ServiceContainer;
using Autofac;

namespace Aggrex.ConsensusProtocol.Transaction
{
    public class TransactionDispatcher : ITransactionDispatcher
    {
        private IObjectReader _reader;

        public TransactionDispatcher(IObjectReader reader)
        {
            _reader = reader;
        }

        public void DispatchTransaction(BinaryReader reader, IRemoteNode remoteNode)
        {
            TransactionType type = (TransactionType)reader.ReadByte();

            switch (type)
            {
                case TransactionType.TransferTransaction:
                    DispatchTransaction<TransferTransaction>(reader, remoteNode);
                    break;
            }
        }

        private void DispatchTransaction<T>(BinaryReader reader, IRemoteNode remoteNode) where T : BaseTransaction, new()
        {
            var transaction = _reader.ReadObject<T>(reader);
            var processor = AggrexContainer.Container.Resolve<ITransactionProcessor<T>>();
            processor.ProcessTransaction(transaction, remoteNode);
        }
    }
}