using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Controllers {
    public class InsuranceCasesController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private const int PAGE_SIZE = 9;

        public InsuranceCasesController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter) {
            _context = context;
            _cache = cache;
            _cookieManager = cookieManager;
            _filter = filter;
        }

        public async Task<IActionResult> Index(int page, int pageSize = PAGE_SIZE) {
            var insuranceCaseFilter = _cookieManager.GetCookie<InsuranceCaseFilterModel>(HttpContext.Request.Cookies);
            insuranceCaseFilter.InsuranceTypeList = GetInsuranceTypeList();
            var insuranceCases = _filter.Filter(insuranceCaseFilter);
            var viewModel = new PageModel<InsuranceCase, InsuranceCaseFilterModel>(page, pageSize, insuranceCases, insuranceCaseFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InsuranceCaseFilterModel insuranceCaseFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(insuranceCaseFilter, HttpContext.Response.Cookies);
            insuranceCaseFilter.InsuranceTypeList = GetInsuranceTypeList();
            var insuranceCases = _filter.Filter(insuranceCaseFilter);
            var viewModel = new PageModel<InsuranceCase, InsuranceCaseFilterModel>(page, pageSize, insuranceCases, insuranceCaseFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceCase = _cache.GetEntity<InsuranceCase>().FirstOrDefault(m => m.Id == id);
            if (insuranceCase == null) {
                return NotFound();
            }
            return View(insuranceCase);
        }

        private IEnumerable<SelectListItem> GetInsuranceTypeList() {
            return _cache.GetEntity<InsuranceType>()
                .Select(e => new SelectListItem() {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
        }
    }
}