using System;
using Microsoft.Azure.WebJobs.Host;

namespace SampleFunction.AzureRedis.AsCollector.Helpers
{
    /// <summary>
    /// Helper class abstracting retrieval of keys from settings store
    /// </summary>
    internal class ConfigurationHelper
    {
        /// <summary>
        /// Returns the value of the environment variable associated with the process. Usually configuration values.
        /// </summary>
        /// <param name="configurationKey">Name of configuration variable</param>
        /// <returns>value of the configuration variable</returns>
        public static string GetConfigurationValue(string configurationKey)
        {
            return Environment.GetEnvironmentVariable(configurationKey, EnvironmentVariableTarget.Process);
        }

        /// <summary>
        /// Returns the value of the connection string from the configuration.
        /// </summary>
        /// <param name="configurationKey">Name of connection </param>
        /// <returns>value of the connection variable</returns>
        public static string GetConnectionString(string connectionName)
        {
            return AmbientConnectionStringProvider.Instance.GetConnectionString(connectionName);
        }
    }
}
