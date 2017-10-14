using Autofac;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Configuration.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationSettingsProvider provider = new ConfigurationSettingsProvider();

            ClientSettings settings = new ClientSettings();
            provider.ReadDefaultConfiguration().GetSection(nameof(ClientSettings)).Bind(settings);

            builder.RegisterInstance(settings)
                .As<ClientSettings>()
                .SingleInstance();

            builder.RegisterInstance(provider)
                .As<IConfigurationSettingsProvider>()
                .SingleInstance();
        }
    }
}