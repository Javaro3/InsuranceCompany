using Microsoft.Extensions.Caching.Memory;

namespace InsuranceCompany.Data.Utilities {
    public class InsuranceCompanyCache {
        private IMemoryCache _cache;
        private InsuranceCompanyContext _db;
        private const int SAVE_DURATION = 300;

        public InsuranceCompanyCache(InsuranceCompanyContext db, IMemoryCache memoryCache) {
            _db = db;
            _cache = memoryCache;
        }

        public IEnumerable<T> GetEntity<T>() {
            var entityName = typeof(T).Name;
            _cache.TryGetValue(entityName, out IEnumerable<T>? entities);

            if (entities is null) {
                entities = SetEntity<T>();
            }
            return entities;
        }

        public IEnumerable<T> SetEntity<T>() {
            var entityName = typeof(T).Name;
            var entities = InsuranceCompanyFactory.GetEnites<T>(entityName, _db);
            _cache.Set(entityName, entities, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(SAVE_DURATION)));
            return entities;
        }
    }
}