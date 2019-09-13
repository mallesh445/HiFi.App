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


        public bool ClearCache()
        {
            //_memoryCache.Remove();
            return true;
        }
    }
}
