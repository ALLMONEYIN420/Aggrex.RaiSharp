using Aggrex.Common.BitSharp;
using Aggrex.Network.Messages.Publish;
using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network
{
    public interface ILedger
    {
        void AddRepresentation(UInt256 repBlockHash, UInt128 amount);
        Block GetBlock(UInt256 blockHash);
        void PutBlock(UInt256 blockHash, SendBlock block, UInt256 successorHash = null);
        void PutBlock(UInt256 blockHash, ReceiveBlock block, UInt256 successorHash = null);
        void PutBlock(UInt256 blockHash, OpenBlock block, UInt256 successorHash = null);
        void PutBlock(UInt256 blockHash, ChangeBlock block, UInt256 successorHash = null);
    }
}