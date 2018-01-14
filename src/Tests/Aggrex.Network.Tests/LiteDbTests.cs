using System.IO;
using Aggrex.Common.BitSharp;
using Aggrex.Configuration;
using Aggrex.Database;
using Aggrex.Database.LiteDB;
using Aggrex.Database.Models;
using Aggrex.Network.Accounts;
using LiteDB;
using Xunit;

namespace Aggrex.Network.Tests
{
    public class LiteDbTests
    {
        private DatabaseSettings _databaseSetting;
        public LiteDbTests()
        {
            BsonMapper.Global.RegisterType<UInt256>
            (
                serialize: (uri) => uri.ToByteArray(),
                deserialize: (bson) => new UInt256(bson.AsBinary)
            );

            _databaseSetting = new DatabaseSettings
            {
                FolderPath = Path.Combine(Path.GetTempPath(), "RaiNodeTests")
            };

            File.Delete(_databaseSetting.FolderPath);
        }

        [Fact]
        public void AddAndGetAccount2()
        {
            IRepository<FrontiersDb> _repo = new LiteDBRepository<FrontiersDb>(_databaseSetting);
            var address = "xrb_39ymww61tksoddjh1e43mprw5r8uu1318it9z3agm7e6f96kg4ndqg9tuds4";
            AccountConverter converter = new AccountConverter();
            var accountUInt256 = converter.DecodeAccount(address);
            FrontiersDb frontierEntry = new FrontiersDb();
            frontierEntry.Account = accountUInt256;
            _repo.Insert(frontierEntry);
            var accountFind = converter.DecodeAccount(address);
            var found = _repo.FindOneById(accountFind);
            Assert.NotNull(found);
        }
    }
}