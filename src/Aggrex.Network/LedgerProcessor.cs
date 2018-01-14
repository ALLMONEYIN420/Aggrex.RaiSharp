using System.IO;
using System.Numerics;
using Aggrex.Common.BitSharp;
using Aggrex.Database;
using Aggrex.Database.Models;
using Aggrex.Network.Exceptions;
using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network
{
    public class LedgerProcessor : ILedgerProcessor
    {
        private ILedger _ledger;
        private IRepository<SendBlocksDb> _sendBlockStore;
        private IRepository<FrontiersDb> _frontierStore;

        public LedgerProcessor(ILedger ledger,
            IRepository<SendBlocksDb> sendBlockStore,
            IRepository<FrontiersDb> frontierStore)
        {
            _ledger = ledger;
            _sendBlockStore = sendBlockStore;
            _frontierStore = frontierStore;
        }

        public void Visit(SendBlock block)
        {
            var hash = block.Hash();

            if (_sendBlockStore.FindOneById(hash) == null)
            {
                return;
            }

            var previous = block.Hashables.Previous;

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