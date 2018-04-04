using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using StackExchange.Redis;

namespace WebJobs.Extensions.AzureRedis.Bindings
{
    /// <summary>
    /// Azure redis collector builder.
    /// </summary>
    public class AzureRedisCollectorBuilder  : IConverter<AzureRedisAttribute, IAsyncCollector<RedisCacheItemsBatch>>
        
    {
        /// <summary>
        /// The config.
        /// </summary>
        private AzureRedisConfiguration _config;

        public AzureRedisCollectorBuilder(AzureRedisConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Convert the specified azureRedisAttribute.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="azureRedisAttribute">Azure redis attribute.</param>
        public IAsyncCollector<RedisCacheItemsBatch> Convert(AzureRedisAttribute azureRedisAttribute)
        {
            if (azureRedisAttribute == null)
            {
                throw new ArgumentNullException(nameof(azureRedisAttribute));
            }
            string resolvedConnectionString = _config.ResolveConnectionString(azureRedisAttribute.ConnectionString);
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(resolvedConnectionString);

            return new AzureRedisAsyncCollector(connectionMultiplexer.GetDatabase());
        }
    }
}