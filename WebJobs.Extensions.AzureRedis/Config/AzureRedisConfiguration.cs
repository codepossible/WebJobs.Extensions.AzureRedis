using System;
using System.Globalization;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Config;

using WebJobs.Extensions.AzureRedis.Bindings;

using StackExchange.Redis;

namespace WebJobs.Extensions.AzureRedis
{
    /// <summary>
    /// Azure redis configuration.
    /// </summary>
    public class AzureRedisConfiguration : IExtensionConfigProvider
    {
        /// <summary>
        /// The azure web jobs azure redis connection string.
        /// </summary>
        internal const string AzureWebJobsAzureRedisConnectionString = "AzureWebJobsAzureRedisConnectionString";

        /// <summary>
        /// The default connection string.
        /// </summary>
        private string _defaultConnectionString;

        /// <summary>
        /// Extension Configuration context 
        /// </summary>
        private ExtensionConfigContext _context;

        /// <summary>
        /// The connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Default constructor 
        /// </summary>
        public AzureRedisConfiguration()
        {
        }

        /// <summary>
        /// Initialize the specified context.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="context">Context.</param>
        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;

            INameResolver nameResolver = context.Config.NameResolver;
            _defaultConnectionString = nameResolver.Resolve(AzureWebJobsAzureRedisConnectionString);

            IConverterManager converterManager = context.Config.GetService<IConverterManager>();
            
            var rule = context.AddBindingRule<AzureRedisAttribute>();             
            rule.BindToCollector<RedisCacheItemsBatch> (typeof(AzureRedisCollectorBuilder), this);            
            rule.BindToInput<IDatabase>(new AzureRedisDatabaseClient(this));                           
        }

        /// <summary>
        /// Validates the connection string.
        /// </summary>
        /// <param name="attribute">Attribute.</param>
        /// <param name="paramType">Parameter type.</param>
        public void ValidateConnectionString(AzureRedisAttribute attribute, Type type)
        {
            if (string.IsNullOrEmpty(ConnectionString) &&
                string.IsNullOrEmpty(attribute.ConnectionString) &&
                string.IsNullOrEmpty(_defaultConnectionString))
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture,
                    "The Azure Redis connection string must be set either via a '{0}' app setting, " +
                                  "via the AzureRedisAttribute.ConnectionString property or " +
                                  "via AzureRedisConfiguration.ConnectionString.",
                                  AzureWebJobsAzureRedisConnectionString));
            }
        }


        /// <summary>
        /// Resolves the connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        /// <param name="attributeConnectionString">Attribute connection string.</param>
        public string ResolveConnectionString(string attributeConnectionString) 
        {

            if (!string.IsNullOrEmpty(attributeConnectionString))
            {
                /* Check if the string is enclosed within the '%' sign indicating subsituting value from App Settings for the key
                 enclosed within the '%' signs */
                if (attributeConnectionString.StartsWith("%") && attributeConnectionString.EndsWith("%"))
                {
                    var appSettingsName = attributeConnectionString.Replace("%", "");
                    INameResolver nameResolver = this._context.Config.NameResolver;
                    this.ConnectionString = nameResolver.Resolve(appSettingsName);
                    return this.ConnectionString;
                }
                else
                {
                    /* Actual connection string specified */
                    return attributeConnectionString;
                }
            }

            if (!string.IsNullOrEmpty(this.ConnectionString)) {
                return ConnectionString;
            }

            /* Use default value */
            return _defaultConnectionString;
        }
    }
}
