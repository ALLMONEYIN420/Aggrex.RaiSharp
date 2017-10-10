using Autofac;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Configuration
{
    public static class ConfigurationContainerBuilder
    {
        public static void BuildContainer(ContainerBuilder builder)
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