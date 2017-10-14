using System.IO;
using Aggrex.Network;

namespace Aggrex.ConsensusProtocol.TransactionProcessors
{
    public interface ITransactionProcessor
    {
        void ProcessTransaction(BinaryReader reader, IRemoteNode remoteNode);
    }
}