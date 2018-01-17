using System.Transactions;
using Aggrex.Common.BitSharp;
using Aggrex.Configuration;
using Autofac;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Database.LiteDB.Modules
{
    public class LiteDBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            BsonMapper.Global.RegisterType<UInt256>
            (
                serialize: (uri) => uri.ToByteArray(),
                deserialize: (bson) => new UInt256(bson.AsBinary)
            );

            ConfigurationSettingsProvider provider = new ConfigurationSettingsProvider();

            DatabaseSettings settings = new DatabaseSettings();
            provider.ReadDefaultConfiguration().GetSection(nameof(DatabaseSettings)).Bind(settings);

            builder.RegisterInstance(settings)
                .As<DatabaseSettings>()
                .SingleInstance();
        }
    }
}