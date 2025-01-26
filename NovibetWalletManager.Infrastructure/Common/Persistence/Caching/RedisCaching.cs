using NovibetWalletManager.Application.Common.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Common.Persistence.Caching
{
    public class RedisCaching : ICacheService
    {

        private readonly IConnectionMultiplexer _redis;

        public RedisCaching(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redis.GetDatabase();
            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, expiration);
        }


        public async Task<T?> GetAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var json = await db.StringGetAsync(key);

            if (json.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
