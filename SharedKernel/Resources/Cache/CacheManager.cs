using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace SharedKernel.Resources.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly IDistributedCache _distributedCache;

        private class CacheObjectName<TData>
        {
            public TData? Type { get; set; }
        }
        public CacheManager(IMemoryCache cache, IDistributedCache distributedCache)
        {
            _cache = cache;
            _distributedCache = distributedCache;
        }
        public void Add(string key, object value, TimeSpan idle = new())
        {
            var cachingPolicy = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(3))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);
            _cache.Set(key, value, cachingPolicy);
        }

        public void Add<TData>(string key, TData value, TimeSpan idle = default)
        {
            var cachingPolicy = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(3))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);
            _cache.Set(key, value, cachingPolicy);
        }

        public void AddToRedis(string key, object value, TimeSpan idle = default)
        {
            var policy =
                new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));
            _distributedCache.SetAsync(key, value, policy);
        }

        public object? Get(string key)
        {
            return _cache.Get(key);
        }

        public object? GetFromRedis<T>(string key, out T value)
        {
           return _distributedCache.TryGetValue(key, out value);
        }
        

        public object? GetList<T>(string key)
        {
            return _cache.Get<List<T>>(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Contains(string key)
        {
            return _cache.TryGetValue(key, out key);
        }

        public bool ContainsList<T>(string key)
        {
            return _cache.TryGetValue<List<T>>(key, out _);
        }
    }
}
