using System;
using System.Collections.Generic;
using System.Text;


namespace WebJobs.Extensions.AzureRedis.Bindings
{
    public class RedisCacheItemsBatch
    {
        private List<IRedisCacheItem> _items = new List<IRedisCacheItem>();

        public List<IRedisCacheItem> Items
        {
            get { return _items; }
        }

    }
}
