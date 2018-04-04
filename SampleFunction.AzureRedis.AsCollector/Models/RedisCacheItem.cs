using StackExchange.Redis;

using WebJobs.Extensions.AzureRedis.Bindings;

namespace SampleFunction.AzureRedis.AsCollector.Models
{
    /// <summary>
    /// Class representing the item to be stored in Azure Redis Cache
    /// </summary>
    internal class RedisCacheItem : IRedisCacheItem
    {
        /// <summary>
        /// Member variable storing the key
        /// </summary>
        private string _redisKey;

        /// <summary>
        /// Member variable storing the value
        /// </summary>
        private string _redisValue;

        /// <summary>
        /// Gets and sets the key
        /// </summary>
        public RedisKey Key
        {
            get { return _redisKey; }
            set { _redisKey = value.ToString(); }
        }

        /// <summary>
        /// Gets and sets the value
        /// </summary>
        public RedisValue Value
        {
            get { return _redisValue; }
            set { _redisValue = value.ToString(); }
        }
    }
}
