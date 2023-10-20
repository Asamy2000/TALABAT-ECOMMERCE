using E_commerce.Core.Entities;
using E_commerce.Core.IRepositories;
using StackExchange.Redis;
using System.Text.Json;

namespace E_commerce.Repos
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;
        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketid)
        {
            var basket = await _database.StringGetAsync(basketid);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket?>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var UpdatedOrCreatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (UpdatedOrCreatedBasket is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
