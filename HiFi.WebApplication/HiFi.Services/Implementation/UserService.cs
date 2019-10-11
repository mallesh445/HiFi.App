using HiFi.Common;
using HiFi.Data.Models;
using HiFi.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services.Implementation
{
    public class UserService: IUserService
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IMemoryCache _memoryCache;
        public UserService(IMemoryCache memoryCache, IRepository<ApplicationUser> repository)
        {
            _memoryCache = memoryCache;
            _repository = repository;
        }

        public async Task<int> TotalUsersCount()
        {
            var data =await SetGetUsersByCache();
            return data.AsQueryable().Count();
        }

        private async Task<IEnumerable<ApplicationUser>> SetGetUsersByCache()
        {
            string cacheKey = CacheKeys.UsersListCache;
            IEnumerable<ApplicationUser> applicationUsers;

            if (!_memoryCache.TryGetValue(cacheKey, out applicationUsers))
            {
                applicationUsers = await _repository.GetAll();
                _memoryCache.Set(cacheKey, applicationUsers,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)));
                //_logger.LogInformation($"{cacheKey} updated from source.");
            }
            return applicationUsers;
        }
    }
}
