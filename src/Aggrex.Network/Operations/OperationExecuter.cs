namespace Aggrex.Network.Operations
{
    public interface IOperationExecuter<T> where T : BaseOperation
    {
        void ExecuteOperation(T operation, IRemoteNode remoteNode);
    }
}