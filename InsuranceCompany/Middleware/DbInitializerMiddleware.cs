using InsuranceCompany.Data;
using InsuranceCompany.Data.DbInitializer;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Identity;

namespace InsuranceCompany.Middleware {
    public class DbInitializerMiddleware {
        private readonly RequestDelegate _next;

        public DbInitializerMiddleware(RequestDelegate next) {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, InsuranceCompanyContext db, InsuranceCompanyIdentityContext identityDb, UserManager<ApplicationUser> userManager) {
            if (!(httpContext.Session.Keys.Contains("database"))) {
                InsuranceCompanyInitializer.Initialize(db, identityDb, userManager);
                httpContext.Session.SetString("database", "initial");
            }
            return _next.Invoke(httpContext);
        }
    }

    public static class DbInitializerMiddlewareExtensions {
        public static IApplicationBuilder UseDbInitializerMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }
    }
}
