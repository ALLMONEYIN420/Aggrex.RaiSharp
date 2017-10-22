using System.IO;
using Aggrex.Network;

namespace Aggrex.ConsensusProtocol.Transactions.Dispatcher
{
    public interface ITransactionDispatcher
    {
        void DispatchTransaction(BinaryReader reader, IRemoteNode remoteNode);
    }
}