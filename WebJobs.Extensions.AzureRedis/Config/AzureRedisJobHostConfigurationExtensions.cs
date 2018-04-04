using Microsoft.Azure.WebJobs.Host;
using System;
using WebJobs.Extensions.AzureRedis;

namespace Microsoft.Azure.WebJobs
{
    public static class AzureRedisJobHostConfigurationExtensions
    {
		public static void UseAzureRedis(this JobHostConfiguration config, 
		                                 AzureRedisConfiguration azureRedisConfig = null) {

			if (config == null) {
				throw new ArgumentNullException(nameof(config));
			}

			if (azureRedisConfig == null) {
				azureRedisConfig = new AzureRedisConfiguration();
			}

            config.RegisterExtensionConfigProvider(azureRedisConfig);

        }
    }
}
