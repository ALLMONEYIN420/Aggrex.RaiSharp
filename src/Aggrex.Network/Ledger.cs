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
        private IRepository<SendBlocksDb> _sendBlocksStore;
        private IRepository<OpenBlocksDb> _openBlocksStore;
        private IRepository<ReceiveBlocksDb> _receievBlocksStore;
        private IRepository<ChangeBlocksDb> _changeBlocksStore;

        public Ledger(IRepository<RepresentativesDb> representativesStore,
            IRepository<SendBlocksDb> sendBlocksStore,
            IRepository<OpenBlocksDb> openBlocksStore,
            IRepository<ReceiveBlocksDb> receievBlocksStore,
            IRepository<ChangeBlocksDb> changeBlocksStore)
        {
            _representativesStore = representativesStore;
            _sendBlocksStore = sendBlocksStore;
            _openBlocksStore = openBlocksStore;
            _receievBlocksStore = receievBlocksStore;
            _changeBlocksStore = changeBlocksStore;
        }
        public void AddRepresentation(UInt256 repBlockHash, UInt128 amount)
        {
            var block = GetBlock(repBlockHash);
            if (block != null && block.Representative != UInt256.Zero)
            {
                var prevSource = _representativesStore.FindOneById(block.Representative);
                if (prevSource != null)
                {
                    prevSource.Weight += amount;
                    _representativesStore.Update(prevSource);
                }
            }
        }

        // TODO This could be more efficient. We should look into optimizing the way blocks
        // are found so we don't have to check every store. It just seems unnecessary.
        public Block GetBlock(UInt256 blockHash)
        {
            var sbEntry = _sendBlocksStore.FindOneById(blockHash);
            if (sbEntry != null)
            {
                return sbEntry.SendBlock;
            }

            var rbEntry = _receievBlocksStore.FindOneById(blockHash);
            if (rbEntry != null)
            {
                return rbEntry.ReceiveBlock;
            }

            var obEntry = _openBlocksStore.FindOneById(blockHash);
            if (obEntry != null)
            {
                return obEntry.OpenBlock;
            }

            var cEntry = _changeBlocksStore.FindOneById(blockHash);
            if (cEntry != null)
            {
                return cEntry.ChangeBlock;
            }

            return null;
        }

        public void PutBlock(UInt256 blockHash, SendBlock block, UInt256 successorHash = null)
        {
            ThrowHelper.Sanity.ThrowIfTrue(block == null || GetBlock(successorHash) != null);
        }

        public void PutBlock(UInt256 blockHash, ReceiveBlock block, UInt256 successorHash = null)
        {
            ThrowHelper.Sanity.ThrowIfTrue(block == null || GetBlock(successorHash) != null);
        }

        public void PutBlock(UInt256 blockHash, OpenBlock block, UInt256 successorHash = null)
        {
            ThrowHelper.Sanity.ThrowIfTrue(block == null || GetBlock(successorHash) != null);
        }

        public void PutBlock(UInt256 blockHash, ChangeBlock block, UInt256 successorHash = null)
        {
            ThrowHelper.Sanity.ThrowIfTrue(block == null || GetBlock(successorHash) != null);
        }
    }
}