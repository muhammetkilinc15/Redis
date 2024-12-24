using StackExchange.Redis;
using System.Text.Json;

namespace RedisEcommerce.Services.Redis
{
    public class RedisCacheService : ICacheService
    {

        private readonly IDatabase _cache;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _cache = redis.GetDatabase();
        }


        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _cache.StringGetAsync(key);
            return string.IsNullOrEmpty(cachedData)
                ? default
                : JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedData = JsonSerializer.Serialize(value);
            await _cache.StringSetAsync(key, serializedData, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
