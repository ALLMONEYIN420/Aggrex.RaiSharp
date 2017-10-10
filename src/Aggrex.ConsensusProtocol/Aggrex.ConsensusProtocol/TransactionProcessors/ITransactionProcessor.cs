using Aggrex.Network;

namespace Aggrex.ConsensusProtocol.Transaction
{
    public interface ITransactionProcessor<T> where T : BaseTransaction
    {
        void ProcessTransaction(T transaction, IRemoteNode remoteNode);
    }
}