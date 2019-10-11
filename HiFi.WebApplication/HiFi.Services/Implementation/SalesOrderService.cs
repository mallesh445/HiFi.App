using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiFi.Common;
using HiFi.Data.Models;
using HiFi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace HiFi.Services.Implementation
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IRepository<OrderHeader> _repository;
        private readonly IMemoryCache _memoryCache;

        public SalesOrderService(IRepository<OrderHeader> repository, IMemoryCache memoryCache)
        {
            _repository=repository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllSalesOrders()
        {
            var data = await SetGetSalesOrderByCache();
            return data;
        }

        private async Task<IEnumerable<OrderHeader>> SetGetSalesOrderByCache()
        {
            string cacheKey = CacheKeys.OrderListCache;
            IEnumerable<OrderHeader> orders;

            if (!_memoryCache.TryGetValue(cacheKey, out orders))
            {
                orders = await _repository.GetAll(); 
                _memoryCache.Set(cacheKey, orders,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)));
                //_logger.LogInformation($"{cacheKey} updated from source.");
            }
            return orders;
        }

        public async Task<int> TotalOrdersCount()
        {
            var data =await SetGetSalesOrderByCache();
            return data.AsQueryable().Count();
        }
    }
}
