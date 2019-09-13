using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<OrderHeader> GetAllSalesOrders()
        {
            var data = SetGetSalesOrderByCache();
            return data;
        }

        private IEnumerable<OrderHeader> SetGetSalesOrderByCache()
        {
            string cacheKey = "OrderList-Cache";
            IEnumerable<OrderHeader> orders;

            if (!_memoryCache.TryGetValue(cacheKey, out orders))
            {
                orders = _repository.GetAll(); 
                _memoryCache.Set(cacheKey, orders,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)));
                //_logger.LogInformation($"{cacheKey} updated from source.");
            }
            return orders;
        }

        public int TotalOrdersCount()
        {
            var data = SetGetSalesOrderByCache();
            return data.AsQueryable().Count();
        }
    }
}
