using System.IO;
using System.Numerics;
using Aggrex.Common.BitSharp;
using Aggrex.Common.ThrowHelpers;
using Aggrex.Common.ThrowHelpers.Exceptions;
using Aggrex.Database;
using Aggrex.Database.Models;
using Aggrex.Network.Messages.Publish.Blocks;
using Aggrex.Network.Models;

namespace Aggrex.Network
{
    public class LedgerProcessor : ILedgerProcessor
    {
        private ILedger _ledger;
        private IRepository<SendBlocksDb> _sendBlockStore;
        private IRepository<FrontiersDb> _frontierStore;
        private IRepository<LatestAccountInfoDb> _latestAccountStore;

        public LedgerProcessor(ILedger ledger,
            IRepository<SendBlocksDb> sendBlockStore,
            IRepository<LatestAccountInfoDb> latestAccountStore,
            IRepository<FrontiersDb> frontierStore)
        {
            _ledger = ledger;
            _sendBlockStore = sendBlockStore;
            _frontierStore = frontierStore;
            _latestAccountStore = latestAccountStore;
        }

        public void Visit(SendBlock block)
        {
            UInt256 hash = block.Hash();

            if (_sendBlockStore.FindOneById(hash) == null)
            {
                return;
            }

            UInt256 previous = block.Hashables.Previous;

            if (_sendBlockStore.FindOneById(previous) == null) // have we seen the previous block before
            {
                return;
            }

            FrontiersDb account = _frontierStore.FindOneById(block.Hashables.Previous);

            if (account == null)
            {
                return;
            }

            //BinaryReader reader = new BinaryReader(new MemoryStream(account.ValueBytes));

            //UInt256 blockHash = new UInt256(reader.ReadBytes(32));
            //UInt256 representative = new UInt256(reader.ReadBytes(32));
            //BigInteger balance = new BigInteger(reader.ReadBytes(17));
            //long timeStamp = reader.ReadInt64();

            if (!Utility.ValidateMessage(account.Account, hash, block.Signature))
            {
                throw new BadSignatureException();
            }

            var accountInfo = _latestAccountStore.FindOneById(account.Account);

            ThrowHelper.Sync.ThrowIfGapDetected(accountInfo.Head, block.Hashables.Previous);

            ThrowHelper.BadActor.ThrowIfOverspendDetected(accountInfo.Balance, block.Hashables.Balance);

            UInt128 amount = accountInfo.Balance - block.Hashables.Balance;

            /*
            	if (result.code == rai::process_result::progress)
				{
					rai::account_info info;
					auto latest_error (ledger.store.account_get (transaction, account, info));
					assert (!latest_error);
					assert (info.head == block_a.hashables.previous);
					result.code = info.balance.number () >= block_a.hashables.balance.number () ? rai::process_result::progress : rai::process_result::overspend; // Is this trying to spend more than they have (Malicious)
					if (result.code == rai::process_result::progress)
					{
						auto amount (info.balance.number () - block_a.hashables.balance.number ());
						ledger.store.representation_add (transaction, info.rep_block, 0 - amount);
						ledger.store.block_put (transaction, hash, block_a);
						ledger.change_latest (transaction, account, hash, info.rep_block, block_a.hashables.balance, info.block_count + 1);
						ledger.store.pending_put (transaction, rai::pending_key (block_a.hashables.destination, hash), {account, amount});
						ledger.store.frontier_del (transaction, block_a.hashables.previous);
						ledger.store.frontier_put (transaction, hash, account);
						result.account = account;
						result.amount = amount;
					}
             */

        }

        public void Visit(ChangeBlock block)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(OpenBlock block)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ReceiveBlock block)
        {
            throw new System.NotImplementedException();
        }
    }
}