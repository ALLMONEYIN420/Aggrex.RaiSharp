using System.Transactions;
using Aggrex.Configuration;
using Aggrex.Database.Models;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Database.LiteDB.Modules
{
    public class LiteDBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationSettingsProvider provider = new ConfigurationSettingsProvider();

            DatabaseSettings settings = new DatabaseSettings();
            provider.ReadDefaultConfiguration().GetSection(nameof(DatabaseSettings)).Bind(settings);

            builder.RegisterInstance(settings)
                .As<DatabaseSettings>()
                .SingleInstance();

            RegisterRepositoriesForModels(builder);
        }

        private void RegisterRepositoriesForModels(ContainerBuilder builder)
        {
            builder.RegisterType<LiteDBRepository<TransferTransactionModel>>()
                .As<IRepository<TransferTransactionModel>>()
                .SingleInstance();
        }
    }
}