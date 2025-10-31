using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService (ICacheRepository cacheRepository): ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            return await cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string Cachekey, object CacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(CacheValue);
            await cacheRepository.SetAsync(Cachekey,value,TimeToLive);
        }
    }
}
