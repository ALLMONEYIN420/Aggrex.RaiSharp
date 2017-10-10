using System.IO;
using Aggrex.Network;

namespace Aggrex.ConsensusProtocol.Transaction
{
    public interface ITransactionDispatcher
    {
        void DispatchTransaction(BinaryReader reader, IRemoteNode remoteNode);
    }
}