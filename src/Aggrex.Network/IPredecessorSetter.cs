using Aggrex.Common.BitSharp;
using Aggrex.Network.Messages.Publish;

namespace Aggrex.Network
{
    public interface IPredecessorSetter
    {
        void FillValue(UInt256 successor, UInt256 predecessor);
    }
}