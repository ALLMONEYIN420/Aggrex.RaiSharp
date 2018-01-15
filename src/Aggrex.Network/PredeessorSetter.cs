using Aggrex.Common.ThrowHelpers;
using Aggrex.Network.Messages.Publish;
using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network
{
    public class PredeessorSetter : IPredecessorSetter
    {
        private ILedger _ledger { get; set; }
        public PredeessorSetter(ILedger ledger)
        {
            ledger = _ledger;
        }

        public void Visit(SendBlock block)
        {
            FillValue(block);
        }

        public void Visit(ChangeBlock block)
        {
            FillValue(block);
        }

        public void Visit(OpenBlock block)
        {
            FillValue(block);
        }

        public void Visit(ReceiveBlock block)
        {
            FillValue(block);
        }

        /*
         * 
         auto hash (block_a.hash ());
		rai::block_type type;
		auto value (store.block_get_raw (transaction, block_a.previous (), type));
		assert (value.mv_size != 0);
		std::vector <uint8_t> data (static_cast <uint8_t *> (value.mv_data), static_cast <uint8_t *> (value.mv_data) + value.mv_size);
		std::copy (hash.bytes.begin (), hash.bytes.end (), data.end () - hash.bytes.size ());
		store.block_put_raw (transaction, store.block_database (type), block_a.previous (), rai::mdb_val (data.size (), data.data()));
         */
        private void FillValue(Block block)
        {
            var hash = block.Hash();
            var prevBlock = _ledger.GetBlock(block.Previous);
            ThrowHelper.Sanity.ThrowIfNull(prevBlock);
        }
    }
}