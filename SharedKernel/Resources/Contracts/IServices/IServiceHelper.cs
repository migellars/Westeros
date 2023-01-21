namespace SharedKernel.Resources.Contracts.IServices;

public interface IServiceHelper 
{
    Task<System.Exception> GetExceptionAsync(string message, string errorCode);
    T GetOrUpdateCacheItem<T>(string key, Func<T> update, TimeSpan idle = default(TimeSpan));
    string GetCurrentUserEmail();
    void RemoveCachedItem(string key);

}