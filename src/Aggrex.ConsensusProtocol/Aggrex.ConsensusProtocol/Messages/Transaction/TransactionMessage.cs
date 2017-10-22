using System.IO;
using Aggrex.ConsensusProtocol.Transactions;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.Messages.Transaction
{
    public class TransactionMessage : BaseMessage 
    {
        public override MessageType MessageType => MessageType.Transaction;

        public BaseTransaction Transaction { get; }

        public TransactionMessage(BaseTransaction transaction)
        {
            Transaction = transaction;
        }

        protected override void WriteProperties(BinaryWriter writer)
        {
            Transaction.WriteToStream(writer);
        }

        protected override void ReadProperties(BinaryReader reader)
        {
            Transaction.ReadFromStream(reader);
        }
    }

    //public class TransactionMessage<T> : TransactionMessage where T : new()
    //{
    //    public override MessageType MessageType => MessageType.Transaction;

    //    public BaseTransaction Transaction { get; }



    //    protected override void WriteProperties(BinaryWriter writer)
    //    {
    //    }

    //    protected override void ReadProperties(BinaryReader reader)
    //    {
    //    }

    //    public TransactionMessage(BaseTransaction transaction) : base(transaction)
    //    {
    //    }
    //}

}