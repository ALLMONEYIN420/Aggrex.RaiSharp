namespace Aggrex.VirtualMachine
{
    public interface IScriptStorageContainer
    {
        IScriptStorageContext GetScriptStorageContext(byte[] scriptHash);
    }
}