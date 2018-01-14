using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Aggrex.Network.Messages.Publish;

namespace Aggrex.Network
{
    public class BlockProcessor : IBlockProcessor
    {
        private BufferBlock<Block> _blockQueue;

        public BlockProcessor()
        {
           _blockQueue = new BufferBlock<Block>();
            Task.Run(() => BlockProcessingLoop());
        }

         async void BlockProcessingLoop()
        {
            while (true)
            {
                Block block = await _blockQueue.ReceiveAsync();
            }
        }

        public void QueueBlock(Block block)
        {
        }
    }
}