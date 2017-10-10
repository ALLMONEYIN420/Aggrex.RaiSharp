namespace Aggrex.VirtualMachine
{
    public interface IInvocationEngine
    {
        void Execute(byte[] scriptHash, params byte[] parameters);
    }
}