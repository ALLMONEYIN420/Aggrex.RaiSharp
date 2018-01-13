using System;
using System.Text;
using Aggrex.ConsensusProtocol.Accounts;
using Blake2Sharp;
using Xunit;

namespace Aggrex.Network.Tests
{
    public class AccountConverterTests
    {
        [Fact]
        public void Passing()
        {
            AccountConverter converter = new AccountConverter();

            var address = "xrb_39ymww61tksoddjh1e43mprw5r8uu1318it9z3agm7e6f96kg4ndqg9tuds4";
            var decode = converter.DecodeAccount(address);
            var encode = converter.EncodeAccount(decode);

            Assert.Equal(address, encode);
        }
    }
}