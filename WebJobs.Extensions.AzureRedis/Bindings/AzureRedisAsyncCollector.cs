using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using StackExchange.Redis;

namespace WebJobs.Extensions.AzureRedis.Bindings
{
    /// <summary>
    /// Implementation of Azure Redis Collector
    /// </summary>
    public class AzureRedisAsyncCollector: IAsyncCollector<RedisCacheItemsBatch>                                                
    {
        /// <summary>
        /// The redis database.
        /// </summary>
        private IDatabase _redisDatabase;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:WebJobs.Extensions.AzureRedis.Bindings.AzureRedisAsyncCollector`1"/> class.
        /// </summary>
        /// <param name="redisDatabase">Redis database.</param>
        public AzureRedisAsyncCollector(IDatabase redisDatabase)
        {
            this._redisDatabase = redisDatabase;
        }
        
        /// <summary>
        /// Adds the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="item">Item.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task AddAsync(RedisCacheItemsBatch redisCacheItemsBatch, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<KeyValuePair<RedisKey, RedisValue>> batch = new List<KeyValuePair<RedisKey, RedisValue>>();
            redisCacheItemsBatch.Items.ForEach((item) => {
                batch.Add(new KeyValuePair<RedisKey, RedisValue>(item.Key, item.Value));
            });
            return _redisDatabase.StringSetAsync(batch.ToArray());
        }

        /// <summary>       
        /// Flushs the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }
    }
}
