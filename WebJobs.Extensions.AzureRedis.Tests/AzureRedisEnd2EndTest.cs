using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

using Xunit;
using StackExchange.Redis;
using System.Collections.Generic;

namespace WebJobs.Extensions.AzureRedis.Tests
{
    public class AzureRedisEnd2EndTest
    {
       // [Fact]
        public async Task Test_InputBindings()
        {

            await RunTestAsync(nameof(AzureRedisEnd2EndTest.Test_InputBindings));
        }

        private async Task RunTestAsync(string testName, object argument = null) 
        {

            Type testType = typeof(AzureRedisEnd2EndTest);

            var azureRedisConfig = new AzureRedisConfiguration();

            JobHostConfiguration config = new JobHostConfiguration()
            {
               DashboardConnectionString = "UseDevelopmentStorage=true",
               StorageConnectionString= "UseDevelopmentStorage=true"
            };

            config.UseAzureRedis(azureRedisConfig);
            JobHost host = new JobHost(config);

            var arguments = new Dictionary<string, object>();

            await host.StartAsync();
            await host.CallAsync(testType.GetMethod(testName), arguments);
            await host.StopAsync();

            
        }


    }
}
