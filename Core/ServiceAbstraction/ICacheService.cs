using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string Cachekey, object CacheValue,TimeSpan TimeToLive);
    }
}
