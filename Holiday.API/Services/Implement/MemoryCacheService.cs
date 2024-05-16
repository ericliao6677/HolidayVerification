using Microsoft.Extensions.Caching.Memory;

namespace Holiday.API.Services.Implement
{
    public class MemoryCacheService
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string AddToMemoryCache(string jti, TimeSpan memoryCacheExpired)
        {
            string cacheEntry = "";

            if (!_cache.TryGetValue(jti, out cacheEntry))
            {
                // 指定的Cache不存在，所以給予一個新的值
                cacheEntry = jti;

                // 設定Cache選項
                var cacheEntryOptions = new MemoryCacheEntryOptions()

                    // 設定Cache保存時間，如果有存取到就會刷新保存時間
                    .SetSlidingExpiration(memoryCacheExpired);

                // 把資料除存進Cache中
                _cache.Set(jti, cacheEntry, cacheEntryOptions);
                
            }

            return cacheEntry;
        }

        public string GetCache(string key)
        {
            if (!_cache.TryGetValue(key, out var cacheEntry))
                return "不再cache裡~~~";

            return cacheEntry.ToString();
        }
    }
}
