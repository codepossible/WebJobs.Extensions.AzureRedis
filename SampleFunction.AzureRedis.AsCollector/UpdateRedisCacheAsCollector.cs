using System;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

using WebJobs.Extensions.AzureRedis;
using WebJobs.Extensions.AzureRedis.Bindings;

namespace SampleFunction.AzureRedis.AsCollector
{
    using Helpers;
    using Models;

    /// <summary>
    /// Sample Azure Function to demonstrate the use of AzureRedisAttribute as Async Collector supporting
    /// writings of cache items as a batch.
    /// </summary>
    public static class UpdateRedisCacheAsCollector
    {
        #region Function Code
        /// <summary>
        /// BLOB Trigger handler to update the Azure Redis Cache based on updated blob
        /// </summary>
        /// <param name="myBlob">File stream from the blob</param>
        /// <param name="redisCache">Reference to Azure Redis as IAsyncCollector</param>
        /// <param name="log">Log Writer</param>
        /// <returns>Async Task</returns>
        [FunctionName("UpdateRedisCacheAsCollector")]
        public static async Task Run(
            [BlobTrigger("%SourceBlob%", Connection = "SourceAzureBlobStorageConnection")]Stream myBlob,
            [AzureRedis]IAsyncCollector<RedisCacheItemsBatch> redisCache,
            TraceWriter log
            )
        {
            log.Info($"Data Changed on Blob - {ConfigurationHelper.GetConfigurationValue("SourceBlob")} \t Size: {myBlob.Length} Bytes");

            try
            {
                int batchSize = int.TryParse(ConfigurationHelper.GetConfigurationValue("BatchSize"), out batchSize) ? batchSize : 1000;
                log.Info($"Processing Batch Size - {batchSize} ");

                int keysWrittenToCache =await ReadBlobAndUpdateRedisCache(myBlob, redisCache, batchSize);

                log.Info($"Process complete. {keysWrittenToCache} keys written to Redis cache");            
            }
            catch (Exception ex)
            {
                log.Error($"Error occurred while processing BLOB changes. Error Info: {ex.ToString()}", ex);
            }

        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Reads the BLOB, updates the Redis Cache in bulk manner based on batch size.
        /// </summary>
        /// <param name="myBlob">BLOB Stream reference</param>
        /// <param name="redisCacheHelper">Redis Cache loader helper</param>
        /// <param name="batchSize">batch size for bulk load</param>
        /// <returns></returns>
        private static async Task<int> ReadBlobAndUpdateRedisCache(Stream myBlob, IAsyncCollector<RedisCacheItemsBatch> asyncCollector, int batchSize)
        {

            int keysWrittenToCache = 0;

            /* Read contents of the updated BLOB */
            using (var reader = new StreamReader(myBlob))
            {
                var counter = 0;
                var batch = new RedisCacheItemsBatch();

                while (!reader.EndOfStream)
                {
                    // assuming data format of xxxx,yyyyy
                    var line = reader.ReadLine()?.Split(new char[] { ',' });

                    /* Ignore lines which could not be read or do not have at least one delimiter. */
                    if (line != null && line.Length > 1)
                    {
                        batch.Items.Add(new RedisCacheItem() { Key = line[0], Value = line[1] });
                        counter++;
                    }

                    /* if the read count has reached batch size, write the key-value pair to Redis Cache 
                       clear the batch and reset the read counter.
                    */
                    if (counter == batchSize)
                    {
                        await asyncCollector.AddAsync(batch);
                        keysWrittenToCache += counter;
                        counter = 0;
                        batch.Items.Clear();
                    }
                }

                /* 
                  if items are still left in the batch, as count could not meet 
                  the batch size, write remaining key-value pair to Redis Cache.
                */
                if (batch.Items.Count > 0)
                {
                    await asyncCollector.AddAsync(batch);
                    keysWrittenToCache += batch.Items.Count;
                    batch.Items.Clear();
                }
            }
            return keysWrittenToCache;
        }

        #endregion
    }
}
