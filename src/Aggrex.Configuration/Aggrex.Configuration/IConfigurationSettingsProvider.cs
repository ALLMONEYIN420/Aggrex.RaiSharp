using Microsoft.Extensions.Configuration;

namespace Aggrex.Configuration
{
    public interface IConfigurationSettingsProvider
    {
        IConfigurationRoot ReadDefaultConfiguration();
        IConfigurationRoot ReadConfiguration(string filePath);
    }
}