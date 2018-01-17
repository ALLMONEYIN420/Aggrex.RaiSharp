using Aggrex.Common.BitSharp;
using Aggrex.Common.ThrowHelpers;
using Aggrex.Database;
using Aggrex.Database.Models;
using Aggrex.Network.Messages.Publish;
using Aggrex.Network.Messages.Publish.Blocks;
using Aggrex.Network.Models;

namespace Aggrex.Network
{
    public class Ledger : ILedger
    {
        private IRepository<RepresentativesDb> _representativesStore;
        private IRepository<SendBlock> _sendBlocksStore;
        private IRepository<OpenBlock> _openBlocksStore;
        private IRepository<ReceiveBlock> _receievBlocksStore;
        private IRepository<ChangeBlock> _changeBlocksStore;

        public Ledger(IRepository<RepresentativesDb> representativesStore,
            IRepository<SendBlock> sendBlocksStore,
            IRepository<OpenBlock> openBlocksStore,
            IRepository<ReceiveBlock> receievBlocksStore,
            IRepository<ChangeBlock> changeBlocksStore)
        {
            _representativesStore = representativesStore;
            _sendBlocksStore = sendBlocksStore;
            _openBlocksStore = openBlocksStore;
            _receievBlocksStore = receievBlocksStore;
            _changeBlocksStore = changeBlocksStore;
        }
        public void AddRepresentation(UInt256 repBlockHash, BlockType blockType, UInt128 amount)
        {
            Block block = null;

            switch (blockType)
            {
                case BlockType.Send:
                    block = GetSendBlock(repBlockHash);
                    break;
                case BlockType.Receive:
                    block = GetReceiveBlock(repBlockHash);
                    break;
                case BlockType.Change:
                    block = GetChangeBlock(repBlockHash);
                    break;
                case BlockType.Open:
                    block = GetOpenBlock(repBlockHash);
                    break;
            }

            if (block == null || block.Representative == UInt256.Zero)
            {
                return;
            }

            var prevSource = _representativesStore.FindOneById(block.Representative);
            if (prevSource != null)
            {
                prevSource.Weight += amount;
                _representativesStore.Update(prevSource);
            }
        }

        public SendBlock GetSendBlock(UInt256 blockHash)
        {
            return _sendBlocksStore.FindOneById(blockHash);
        }

        public OpenBlock GetOpenBlock(UInt256 blockHash)
        {
            return _openBlocksStore.FindOneById(blockHash);
        }

        public ChangeBlock GetChangeBlock(UInt256 blockHash)
        {
            return _changeBlocksStore.FindOneById(blockHash);
        }

        public ReceiveBlock GetReceiveBlock(UInt256 blockHash)
        {
            return _receievBlocksStore.FindOneById(blockHash);
        }

        // TODO This could be more efficient. We should look into optimizing the way blocks
        // are found so we don't have to check every store. It just seems unnecessary.
        public Block SearchForBlock(UInt256 blockHash)
        {
            var sbEntry = _receievBlocksStore.FindOneById(blockHash);
            if (sbEntry != null)
            {
                return sbEntry;
            }

            var rbEntry = _receievBlocksStore.FindOneById(blockHash);
            if (rbEntry != null)
            {
                return rbEntry;
            }

            var obEntry = _openBlocksStore.FindOneById(blockHash);
            if (obEntry != null)
            {
                return obEntry;
            }

            var cEntry = _changeBlocksStore.FindOneById(blockHash);
            if (cEntry != null)
            {
                return cEntry;
            }

            return null;
        }

        public void AddBlock(SendBlock block)
        {
            ThrowHelper.Sanity.ThrowIfNull(block);
        }

        public void AddBlock(ReceiveBlock block)
        {
            ThrowHelper.Sanity.ThrowIfNull(block);
        }

        public void AddBlock(OpenBlock block)
        {
            ThrowHelper.Sanity.ThrowIfNull(block);
        }

        public void AddBlock(ChangeBlock block)
        {
            ThrowHelper.Sanity.ThrowIfNull(block);
        }

        public void UpdateBlock(SendBlock block)
        {
            _sendBlocksStore.Update(block);
        }

        public void UpdateBlock(ReceiveBlock block)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBlock(OpenBlock block)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBlock(ChangeBlock block)
        {
            throw new System.NotImplementedException();
        }
    }
}