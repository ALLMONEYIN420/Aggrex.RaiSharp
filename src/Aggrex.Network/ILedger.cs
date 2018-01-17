using Aggrex.Common.BitSharp;
using Aggrex.Network.Messages.Publish;
using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network
{
    public interface ILedger
    {
        void AddRepresentation(UInt256 repBlockHash, BlockType type, UInt128 amount);
        Block SearchForBlock(UInt256 blockHash);


        SendBlock GetSendBlock(UInt256 blockHash);
        OpenBlock GetOpenBlock(UInt256 blockHash);
        ChangeBlock GetChangeBlock(UInt256 blockHash);
        ReceiveBlock GetReceiveBlock(UInt256 blockHash);

        void AddBlock(SendBlock block);
        void AddBlock(ReceiveBlock block);
        void AddBlock(OpenBlock block);
        void AddBlock(ChangeBlock block);

        void UpdateBlock(SendBlock block);
        void UpdateBlock(ReceiveBlock block);
        void UpdateBlock(OpenBlock block);
        void UpdateBlock(ChangeBlock block);
    }
}