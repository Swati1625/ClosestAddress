namespace ClosestAddress.Cache
{
    public interface ICustomCache
    {
        void Add<T>(T o, string key, double chacheDuration);
        void Clear(string key);
        bool Exists(string key);
        bool Get<T>(string key, out T value);
        string GetCacheKey(params object[] keyComponents);
    }
}
