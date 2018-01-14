using Aggrex.Network.Messages.Publish;

namespace Aggrex.Network
{
    public interface IBlockProcessor
    {
        void QueueBlock(Block block);
    }
}