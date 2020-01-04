using System;
using System.Text;
using System.Web;

namespace ClosestAddress.Cache
{
    public  class CustomCache : ICustomCache
    {
        private const string Separator = "|";
        public  string GetCacheKey(params object[] keyComponents)
        {
            var cacheKey = new StringBuilder();
            foreach (var keyComponent in keyComponents)
            {
                if (cacheKey.Length > 0)
                    cacheKey.Append(Separator);
                cacheKey.Append(keyComponent);
            }
            return cacheKey.ToString();
        }
        public void Add<T>(T o, string key, double chacheDuration)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Insert(
                    key,
                    o,
                    null,
                    DateTime.Now.AddMinutes(chacheDuration),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }
        public void Clear(string key)
        {
            if (HttpContext.Current != null)
            {
                if (Exists(key))
                {
                    HttpContext.Current.Cache.Remove(key);
                }
            }
        }
        public bool Exists(string key)
        {
            if (HttpContext.Current == null)
                return false;

            return HttpContext.Current.Cache[key] != null;
        }
        public bool Get<T>(string key, out T value)
        {
            if (HttpContext.Current == null)
            {
                value = default(T);
                return false;
            }

            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return false;
                }

                value = (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                value = default(T);
                return false;
            }
            return true;
        }
    }
}
