using System.Text.Json;

namespace InsuranceCompany.Services {
    public class InsuranceCompanyCookieManager {
        public void SetCookie<T>(T Entity, IResponseCookies cookies) {
            var json = JsonSerializer.Serialize(Entity);
            string key = typeof(T).Name;
            cookies.Append(key, json);
        }

        public T GetCookie<T>(IRequestCookieCollection cookies) where T : new() {
            string key = typeof(T).Name;
            if (cookies.TryGetValue(key, out var json)) {
                return JsonSerializer.Deserialize<T>(json);
            }
            return new T();
        }
    }
}
