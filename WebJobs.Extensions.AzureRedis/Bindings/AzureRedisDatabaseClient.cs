using System;
using Microsoft.Azure.WebJobs;
using StackExchange.Redis;


namespace WebJobs.Extensions.AzureRedis.Bindings
{
    /// <summary>
    /// Azure redis database client.
    /// </summary>
	public class AzureRedisDatabaseClient: IConverter<AzureRedisAttribute, IDatabase>
    {
        /// <summary>
        /// The config.
        /// </summary>
        private AzureRedisConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:WebJobs.Extensions.AzureRedis.Bindings.AzureRedisDatabaseClient"/> class.
        /// </summary>
        /// <param name="config">Config.</param>
        public AzureRedisDatabaseClient(AzureRedisConfiguration config)
        {
            this._config = config;
        }

        /// <summary>
        /// Convert the specified azureRedisAttribute.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="azureRedisAttribute">Azure redis attribute.</param>
        public IDatabase Convert(AzureRedisAttribute azureRedisAttribute)
        {
            if (azureRedisAttribute == null)
            {
                throw new ArgumentNullException(nameof(azureRedisAttribute));
            }
            string resolvedConnectionString = _config.ResolveConnectionString(azureRedisAttribute.ConnectionString);
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(resolvedConnectionString);

            return connectionMultiplexer.GetDatabase();
        }
    }
}
