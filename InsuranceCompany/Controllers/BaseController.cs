using InsuranceCompany.Services;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service;

namespace InsuranceCompany.Controllers {
    public class BaseController : Controller {
        protected readonly InsuranceCompanyContext _context;
        protected readonly InsuranceCompanyCache _cache;
        protected readonly InsuranceCompanyCookieManager _cookieManager;
        protected readonly InsuranceCompanyFilter _filter;
        
        public BaseController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter) {
            _context = context;
            _cache = cache;
            _cookieManager = cookieManager;
            _filter = filter;
        }
    }
}
