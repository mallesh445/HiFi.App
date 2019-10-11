using HiFi.Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Services.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Clears the Cache.
        /// </summary>
        /// <returns></returns>
        public bool ClearCache()
        {
            var keys=GetAllKeys();
            foreach (var key in keys)
            {
                _memoryCache.Remove(key);
            }
            return true;
        }

        /// <summary>
        /// Get all keys stored in cache.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllKeys()
        {
            List<string> list = new List<string>();
            list.Add(CacheKeys.CategoryNavListCache);
            list.Add(CacheKeys.OrderListCache);
            list.Add(CacheKeys.ProductsListCache);
            return list;
        }
    }
}
