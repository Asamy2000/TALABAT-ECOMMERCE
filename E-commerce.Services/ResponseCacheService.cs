﻿using E_commerce.Core.IServices;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class ResponseCacheService : IResponseCacheService
    {

        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cachekey, object response, TimeSpan timeToLive)
        {
            if (response == null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serilizedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cachekey, serilizedResponse, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string cachekey)
        {
            var cachedResponse = await _database.StringGetAsync(cachekey);

            if (cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }
    }
}
