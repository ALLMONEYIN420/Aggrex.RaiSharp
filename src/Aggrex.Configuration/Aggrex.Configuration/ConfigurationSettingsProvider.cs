using System.IO;
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Aggrex.Configuration
{
    public class ConfigurationSettingsProvider : IConfigurationSettingsProvider
    {
        public IConfigurationRoot ReadDefaultConfiguration()
        {
            return ReadConfiguration("appSettings.json");
        }

        public IConfigurationRoot ReadConfiguration(string fileName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
