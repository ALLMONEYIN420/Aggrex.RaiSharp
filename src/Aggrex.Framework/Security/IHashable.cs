using Blake2Sharp;

namespace Aggrex.Framework.Security
{
    public interface IHashable
    {
        void Hash(Blake2BConfig config, byte[] message);
    }
}