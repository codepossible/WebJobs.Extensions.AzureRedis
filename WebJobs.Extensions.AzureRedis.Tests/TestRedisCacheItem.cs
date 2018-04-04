using System;
using StackExchange.Redis;
using WebJobs.Extensions.AzureRedis.Bindings;

namespace WebJobs.Extensions.AzureRedis.Tests
{
	internal class TestRedisCacheItem : IRedisCacheItem
    {
		private string _redisKey;
		private string _redisValue;

		public RedisKey Key
		{
			get { return _redisKey; }
			set { _redisKey = value.ToString(); }
		}

		public RedisValue Value
		{
			get { return _redisValue; }
			set { _redisValue = value.ToString(); }
		}
	}
}
