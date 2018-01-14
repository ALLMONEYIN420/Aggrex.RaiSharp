using Aggrex.Network.Messages.Publish.Blocks;

namespace Aggrex.Network
{
    public interface IBlockVisitor
    {
        void Visit(SendBlock block);
        void Visit(ChangeBlock block);
        void Visit(OpenBlock block);
        void Visit(ReceiveBlock block);
    }
}