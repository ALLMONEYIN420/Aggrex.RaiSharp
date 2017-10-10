namespace Aggrex.VirtualMachine
{
    public interface IScriptContainer
    {
        byte[] GetScriptByHash(byte[] scriptHash);
    }
}