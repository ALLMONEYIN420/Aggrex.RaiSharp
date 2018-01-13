using Blake2Sharp;

namespace Aggrex.Framework.Security
{
    public interface IHashTable
    {
        void Hash(Blake2BConfig config, byte[] message);
    }
}