using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using InsuranceCompany.Utilities;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Клиент")]
    public class ClientInsuranceCasesController : BaseController, IUpdateCache {
        private UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 9;

        public ClientInsuranceCasesController(
            InsuranceCompanyContext context, 
            InsuranceCompanyCache cache, 
            InsuranceCompanyCookieManager cookieManager, 
            InsuranceCompanyFilter filter,
            UserManager<ApplicationUser> userManager) : base(context, cache, cookieManager, filter) {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page, int pageSize = PAGE_SIZE) {
            var insuranceCaseFilter = _cookieManager.GetCookie<InsuranceCaseFilterModel>(HttpContext.Request.Cookies, "client");
            insuranceCaseFilter.InsuranceTypeList = GetInsuranceTypeList();
            var client = GetClient();
            var insuranceCases = _filter.Filter(insuranceCaseFilter).Where(e => e.ClientId == client.Id);
            var viewModel = new PageModel<InsuranceCase, InsuranceCaseFilterModel>(page, pageSize, insuranceCases, insuranceCaseFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InsuranceCaseFilterModel insuranceCaseFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(insuranceCaseFilter, HttpContext.Response.Cookies, "client");
            insuranceCaseFilter.InsuranceTypeList = GetInsuranceTypeList();
            var client = GetClient();
            var insuranceCases = _filter.Filter(insuranceCaseFilter).Where(e => e.ClientId == client.Id);
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

        public IActionResult Create() {
            ViewData["SupportingDocumentId"] = GetSupportingDocumentsList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceCase insuranceCase) {
            var client = GetClient();
            insuranceCase.ClientId = client.Id;

            if (ModelState.IsValid) {
                _context.Add(insuranceCase);
                await _context.SaveChangesAsync();
                UpdateCache();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupportingDocumentId"] = GetSupportingDocumentsList();
            return View(insuranceCase);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceCase = _cache.GetEntity<InsuranceCase>().FirstOrDefault(e => e.Id == id);
            if (insuranceCase == null) {
                return NotFound();
            }
            ViewData["SupportingDocumentId"] = GetSupportingDocumentsList();
            return View(insuranceCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceCase insuranceCase) {
            if (id != insuranceCase.Id) {
                return NotFound();
            }

            var client = GetClient();
            insuranceCase.ClientId = client.Id;

            if (ModelState.IsValid) {
                try {
                    _context.Update(insuranceCase);
                    UpdateCache();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupportingDocumentId"] = GetSupportingDocumentsList();
            return View(insuranceCase);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceCase = _cache.GetEntity<InsuranceCase>().FirstOrDefault(m => m.Id == id);
            if (insuranceCase == null) {
                return NotFound();
            }

            return View(insuranceCase);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var insuranceCase = _cache.GetEntity<InsuranceCase>().FirstOrDefault(e => e.Id == id);
            if (insuranceCase != null) {
                _context.InsuranceCases.Remove(insuranceCase);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetSupportingDocumentsList() {
            return _cache.GetEntity<SupportingDocument>()
                .Select(e => new SelectListItem() {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
        }

        private IEnumerable<SelectListItem> GetInsuranceTypeList() {
            return _cache.GetEntity<InsuranceType>()
                .Select(e => new SelectListItem() {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
        }

        private Client GetClient() {
            var applicationUser = _userManager.GetUserAsync(HttpContext.User).Result;
            return _cache.GetEntity<Client>()
                .FirstOrDefault(e =>
                    applicationUser.Name == e.Name &&
                    applicationUser.Surname == e.Surname &&
                    applicationUser.MiddleName == e.MiddleName);

        }

        public void UpdateCache() {
            _cache.SetEntity<InsuranceCase>();
        }
    }
}
