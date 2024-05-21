using Microsoft.Extensions.Caching.Distributed;

namespace DayStory.Application.Interfaces;

public interface ICacheService
{
    Task<string> GetStringAsync(string key);
    Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options);
    Task<byte[]> GetAsync(string key);
    Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options);
    Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory, DistributedCacheEntryOptions options);
}
