using Aggrex.Common.BitSharp;

namespace Aggrex.ConsensusProtocol.Accounts
{
    public interface IAccountConverter
    {
        UInt256 DecodeAccount(string account);
        string EncodeAccount(UInt256 account);
    }
}