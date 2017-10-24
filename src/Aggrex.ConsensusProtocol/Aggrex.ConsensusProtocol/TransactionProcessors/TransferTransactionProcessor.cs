using System;
using System.IO;
using Aggrex.ConsensusProtocol.Transactions;
using Aggrex.Database;
using Aggrex.Database.Models;
using Aggrex.Network;
using Aggrex.Network.ObjectReader;
using Autofac.Features.AttributeFilters;

namespace Aggrex.ConsensusProtocol.TransactionProcessors
{
    public class TransferTransactionProcessor : ITransactionProcessor
    {
        private IObjectReader _objectReader;
        private IRepository<TransferTransactionModel> _transferTransactionRepository;

        public TransferTransactionProcessor(
            IRepository<TransferTransactionModel> transferTransactionRepository,
            IObjectReader objectReader)
        {
            _transferTransactionRepository = transferTransactionRepository;
            _objectReader = objectReader;
        }

        public void ProcessTransaction(TransferTransaction transaction, IRemoteNode remoteNode)
        {
            Console.WriteLine("Received a transfer transaction!");
            _transferTransactionRepository.Insert(new TransferTransactionModel
            {
                Amount = transaction.Amount
            });
        }

        public void ProcessTransaction(BinaryReader reader, IRemoteNode remoteNode)
        {
            ProcessTransaction(_objectReader.ReadObject<TransferTransaction>(reader), remoteNode);
        }
    }
}