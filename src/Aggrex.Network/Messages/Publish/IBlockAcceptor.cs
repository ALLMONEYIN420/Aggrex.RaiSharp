namespace Aggrex.Network.Messages.Publish
{
    internal interface IBlockAcceptor
    {
        void Accept(IBlockVisitor visitor);
    }
}