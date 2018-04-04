using Moq;
using Xunit;
using StackExchange.Redis;
using System.Threading.Tasks;
using WebJobs.Extensions.AzureRedis.Bindings;
using System.Collections.Generic;

namespace WebJobs.Extensions.AzureRedis.Tests
{
    /// <summary>
    /// Azure redis async collector test.
    /// </summary>
	[Trait("Category", "AzureRedis")]
    public class AzureRedisAsyncCollectorTest
    {
		/// <summary>
        /// Tests the add async.
        /// </summary>
        /// <returns>The add async.</returns>
        [Fact]
        public async Task Test_AddAsync()
        {
			var mockAzureRedisService = new Mock<IDatabase>(MockBehavior.Strict);
			mockAzureRedisService
				.Setup(m => m.StringSetAsync(It.IsAny<KeyValuePair<RedisKey, RedisValue>[]>(), When.Always, CommandFlags.None))
				.ReturnsAsync(true);

			var collector = new AzureRedisAsyncCollector(mockAzureRedisService.Object);

            var batch = new RedisCacheItemsBatch();
            TestRedisCacheItem item = new TestRedisCacheItem() { Key = "greetings", Value = "hello, world" };
            batch.Items.Add(item);

            await collector.AddAsync(batch);

			mockAzureRedisService.VerifyAll();          
        }   

        /// <summary>
        /// Tests the flush async.
        /// </summary>
        /// <returns>The flush async.</returns>
		[Fact]
        public async Task Test_FlushAsync()
        {
			var mockAzureRedisService = new Mock<IDatabase>(MockBehavior.Strict);         
            var collector = new AzureRedisAsyncCollector(mockAzureRedisService.Object);

			await collector.FlushAsync();

			mockAzureRedisService.VerifyAll();
          
        }   
    }
}
