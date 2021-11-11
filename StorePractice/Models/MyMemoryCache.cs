using Microsoft.Extensions.Caching.Memory;
using System;

namespace StorePractice.Models
{
    public class MyMemoryCache<Item>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = 1024
        });
        
        public Item GetOrCreate(string key, Item item)
        {
            Item cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = item;

                _cache.Set(key, cacheEntry);
            }
            return cacheEntry;
        }
    }
}
