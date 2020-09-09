using System;
using System.Configuration;

namespace SearchFight.Common.Helpers
{
    public static class Configuration
    {
        public static string GetConfigValue(string configName)
        {
            var config = ConfigurationManager.AppSettings[configName];
            if (string.IsNullOrEmpty(config))
                throw new ApplicationException("Missing value for config: " + configName);
            return config;
        }        
    }
}
