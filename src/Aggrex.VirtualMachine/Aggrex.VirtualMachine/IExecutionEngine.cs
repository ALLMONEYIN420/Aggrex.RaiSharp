namespace Aggrex.VirtualMachine
{
    public interface IExecutionEngine
    {
        void Execute(ExecutionContext initialContext);
    }
}