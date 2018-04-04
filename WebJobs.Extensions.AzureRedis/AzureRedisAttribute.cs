using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace WebJobs.Extensions.AzureRedis
{
    /// <summary>
    /// Azure redis attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class AzureRedisAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the connection string setting.
        /// </summary>
        /// <value>The connection string setting.</value>       
        public string ConnectionString { set; get; }      
    }
}
