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
    public class ContractsController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private const int PAGE_SIZE = 9;

        public ContractsController(
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
            var contractFilter = _cookieManager.GetCookie<ContractFilterModel>(HttpContext.Request.Cookies);
            var contracts = _filter.Filter(contractFilter);
            var viewModel = new PageModel<Contract, ContractFilterModel>(page, pageSize, contracts, contractFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContractFilterModel contractFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(contractFilter, HttpContext.Response.Cookies);
            var contracts = _filter.Filter(contractFilter);
            var viewModel = new PageModel<Contract, ContractFilterModel>(page, pageSize, contracts, contractFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contract = _cache.GetEntity<Contract>().FirstOrDefault(m => m.Id == id);
            if (contract == null) {
                return NotFound();
            }
            contract.InsuranceAgents = _cache.GetEntity<InsuranceAgent>().Where(e => e.ContractId == id).ToList();
            return View(contract);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract) {
            if (ModelState.IsValid) {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                UpdateCache();
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contract = _cache.GetEntity<Contract>().FirstOrDefault(e => e.Id == id);
            if (contract == null) {
                return NotFound();
            }
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract) {
            if (id != contract.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                    UpdateCache();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contract = _cache.GetEntity<Contract>().FirstOrDefault(e => e.Id == id);
            if (contract == null) {
                return NotFound();
            }

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var contract = _cache.GetEntity<Contract>().FirstOrDefault(e => e.Id == id);
            if (contract != null) {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        private void UpdateCache() {
            _cache.SetEntity<Contract>();
            _cache.SetEntity<InsuranceAgent>();
            _cache.SetEntity<Policy>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}