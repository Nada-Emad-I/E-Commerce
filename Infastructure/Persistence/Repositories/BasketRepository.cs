using DomainLayer.Contracts;
using DomainLayer.Models.BasketModules;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database=connection.GetDatabase();
        public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null)
        {
         var jsonBasket=JsonSerializer.Serialize(basket);
            var isCreateOrUpdate= await _database.StringSetAsync(basket.Id, jsonBasket, TimeToLive ?? TimeSpan.FromDays(3));
            if (isCreateOrUpdate)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _database.KeyDeleteAsync(Key);
        }

        public async Task<Basket?> GetBasketAsync(string Key)
        {
            var basket =await _database.StringGetAsync(Key);
            if (basket.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<Basket>(basket!);
        }
    }
}
