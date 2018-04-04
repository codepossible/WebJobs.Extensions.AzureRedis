using StackExchange.Redis;

namespace WebJobs.Extensions.AzureRedis.Bindings
{
    /// <summary>
    /// Redis cache item.
    /// </summary>
    public interface IRedisCacheItem
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        RedisKey Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        RedisValue Value { get; set; }

    }
}
