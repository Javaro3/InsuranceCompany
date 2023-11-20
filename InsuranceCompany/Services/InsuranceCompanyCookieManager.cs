using System.Text.Json;

namespace InsuranceCompany.Services {
    public class InsuranceCompanyCookieManager {
        public void SetCookie<T>(T Entity, IResponseCookies cookies, string key = null) {
            var json = JsonSerializer.Serialize(Entity);
            key = key == null ? typeof(T).Name : typeof(T).Name+key;
            cookies.Append(key, json);
        }

        public T GetCookie<T>(IRequestCookieCollection cookies, string key = null) where T : new() {
            key = key == null ? typeof(T).Name : typeof(T).Name + key;
            if (cookies.TryGetValue(key, out var json)) {
                return JsonSerializer.Deserialize<T>(json);
            }
            return new T();
        }
    }
}
