namespace SharedKernel.Resources.Cache
{
    public interface ICacheManager
    {
        void Add<TData>(string key, TData value, TimeSpan idle = default(TimeSpan));
        void Add(string key, object value, TimeSpan idle = default(TimeSpan));
        void AddToRedis(string key, object value, TimeSpan idle = default);
        object? Get(string key);
        object? GetFromRedis<T>(string key, out T? value);
        object? GetList<T>(string key);
        void Remove(string key);
        bool Contains(string key);
        bool ContainsList<T>(string key);
    }
}