using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class InsuranceTypesController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private const int PAGE_SIZE = 9;

        public InsuranceTypesController(
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
            var insuranceTypeFilter = _cookieManager.GetCookie<InsuranceTypeFilterModel>(HttpContext.Request.Cookies);
            var insuranceTypes = _filter.Filter(insuranceTypeFilter);
            var viewModel = new PageModel<InsuranceType, InsuranceTypeFilterModel>(page, pageSize, insuranceTypes, insuranceTypeFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InsuranceTypeFilterModel insuranceTypeFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(insuranceTypeFilter, HttpContext.Response.Cookies);
            var insuranceTypes = _filter.Filter(insuranceTypeFilter);
            var viewModel = new PageModel<InsuranceType, InsuranceTypeFilterModel>(page, pageSize, insuranceTypes, insuranceTypeFilter);

            return View(viewModel);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceType insuranceType) {
            if (ModelState.IsValid) {
                _context.Add(insuranceType);
                await _context.SaveChangesAsync();
                UpdateCache();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceTypes = _cache.GetEntity<InsuranceType>().FirstOrDefault(m => m.Id == id);
            if (insuranceTypes == null) {
                return NotFound();
            }

            insuranceTypes.Policies = _cache.GetEntity<Policy>().Where(e => e.InsuranceTypeId == id).ToList();
            return View(insuranceTypes);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceType = _cache.GetEntity<InsuranceType>().FirstOrDefault(e => e.Id == id);
            if (insuranceType == null) {
                return NotFound();
            }
            return View(insuranceType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceType insuranceType) {
            if (id != insuranceType.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(insuranceType);
                    await _context.SaveChangesAsync();
                    UpdateCache();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceType = _cache.GetEntity<InsuranceType>().FirstOrDefault(e => e.Id == id);
            if (insuranceType == null) {
                return NotFound();
            }

            return View(insuranceType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var insuranceType = _cache.GetEntity<InsuranceType>().FirstOrDefault(e => e.Id == id);
            if (insuranceType != null) {
                _context.InsuranceTypes.Remove(insuranceType);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        private void UpdateCache() {
            _cache.SetEntity<InsuranceType>();
            _cache.SetEntity<Policy>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}