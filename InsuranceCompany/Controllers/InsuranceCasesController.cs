using InsuranceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Models.ViewModels.FilterViewModels;
using Models.ViewModels.PageViewModels;
using Service;
using System.Data;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class InsuranceCasesController : Controller {
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private const int PAGE_SIZE = 9;

        public InsuranceCasesController(
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter) {
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