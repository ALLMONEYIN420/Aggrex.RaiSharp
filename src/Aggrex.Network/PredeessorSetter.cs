using Aggrex.Common.BitSharp;
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
            _ledger = ledger;
        }

        public void FillValue(UInt256 successor, UInt256 predecessor)
        {
            var prevBlock = _ledger.SearchForBlock(predecessor);
            ThrowHelper.Sanity.ThrowIfNull(prevBlock);
            prevBlock.Successor = successor;
        }
    }
}