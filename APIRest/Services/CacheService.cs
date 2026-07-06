using System.Text.Json;
using APIRest.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace APIRest.Services;

public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await distributedCache.GetStringAsync(key);
        return data is null ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var data = JsonSerializer.Serialize(value);
        await distributedCache.SetStringAsync(key, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        });
    }

    public async Task RemoveAsync(string key)
    {
        await distributedCache.RemoveAsync(key);
    }
}
