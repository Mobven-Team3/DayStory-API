using DayStory.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DayStory.Application.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _defaultOptions;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        _defaultOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
    }

    public async Task<string> GetStringAsync(string key)
    {
        var value = await _distributedCache.GetStringAsync(key);
        if (value != null)
        {
            return value;
        }
        else
            throw new ArgumentNullException("Cache missed");
    }

    public async Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options)
    {
        await _distributedCache.SetStringAsync(key, value, options ?? _defaultOptions);
    }

    public async Task<byte[]> GetAsync(string key)
    {
        var value = await _distributedCache.GetAsync(key);
        if (value != null)
        {
            return value;
        }
        else
            throw new ArgumentNullException("Cache missed");

    }

    public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        await _distributedCache.SetAsync(key, value, options ?? _defaultOptions);
    }

    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory, DistributedCacheEntryOptions options)
    {
        var cachedValue = await _distributedCache.GetAsync(key);
        if (cachedValue != null)
        {
            var value = JsonSerializer.Deserialize<T>(cachedValue);
            if (value != null)
            {
                return value;
            }
            else
                throw new ArgumentNullException("Cache missed");
        }

        var newValue = await valueFactory() ?? throw new ArgumentNullException(nameof(valueFactory), "Value factory returned null.");
        await _distributedCache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(newValue), options ?? _defaultOptions);
        return newValue;
    }
}
