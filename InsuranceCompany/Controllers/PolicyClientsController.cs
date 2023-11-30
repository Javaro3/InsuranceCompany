using InsuranceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Models.ViewModels.FilterViewModels;
using Models.ViewModels.PageViewModels;
using Repository;
using Service;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Клиент")]
    public class PolicyClientsController : BaseController, IUpdateCache {
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 9;

        public PolicyClientsController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter,
            UserManager<ApplicationUser> userManager) : base(context, cache, cookieManager, filter) {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page, int pageSize = PAGE_SIZE) {
            var policyFilter = _cookieManager.GetCookie<PolicyFilterModel>(HttpContext.Request.Cookies, "client");
            policyFilter.InsuranceAgentList = GetInsuranceAgentsList();
            policyFilter.InsuranceTypeList = GetInsuranceTypesList();
            var client = GetClient();
            var policies = _filter.Filter(policyFilter, client.Id);
            var viewModel = new PageModel<Policy, PolicyFilterModel>(page, pageSize, policies, policyFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PolicyFilterModel policyFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(policyFilter, HttpContext.Response.Cookies, "client");
            policyFilter.InsuranceAgentList = GetInsuranceAgentsList();
            policyFilter.InsuranceTypeList = GetInsuranceTypesList();
            var client = GetClient();
            var policies = _filter.Filter(policyFilter, client.Id);
            var viewModel = new PageModel<Policy, PolicyFilterModel>(page, pageSize, policies, policyFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var policy = _cache.GetEntity<Policy>().FirstOrDefault(m => m.Id == id);
            if (policy == null) {
                return NotFound();
            }

            return View(policy);
        }

        public async Task<IActionResult> Create(int page, int pageSize = PAGE_SIZE) {
            var policyFilter = _cookieManager.GetCookie<PolicyFilterModel>(HttpContext.Request.Cookies, "client");
            policyFilter.InsuranceAgentList = GetInsuranceAgentsList();
            policyFilter.InsuranceTypeList = GetInsuranceTypesList();
            var policies = _filter.Filter(policyFilter);
            var viewModel = new PageModel<Policy, PolicyFilterModel>(page, pageSize, policies, policyFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PolicyFilterModel policyFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(policyFilter, HttpContext.Response.Cookies, "client");
            policyFilter.InsuranceAgentList = GetInsuranceAgentsList();
            policyFilter.InsuranceTypeList = GetInsuranceTypesList();
            var policies = _filter.Filter(policyFilter);
            var viewModel = new PageModel<Policy, PolicyFilterModel>(page, pageSize, policies, policyFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Add(int? id) {
            if (id == null) {
                return NotFound();
            }

            var policy = _cache.GetEntity<Policy>().FirstOrDefault(e => e.Id == id);
            if (policy == null) {
                return NotFound();
            }

            return View(policy);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id) {
            var policy = _cache.GetEntity<Policy>().FirstOrDefault(e => e.Id == id);
            var client = GetClient();
            if (policy == null) {
                return NotFound();
            }

            _context.Add(new PolicyClient() { ClientId = client.Id, PolicyId = policy.Id });
            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var policy = _cache.GetEntity<Policy>().FirstOrDefault(m => m.Id == id);
            if (policy == null) {
                return NotFound();
            }

            return View(policy);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var clientId = GetClient().Id;
            var policyClient = _cache.GetEntity<PolicyClient>().FirstOrDefault(e => e.PolicyId == id && e.ClientId == clientId);
            if (policyClient != null) {
                _context.PolicyClients.Remove(policyClient);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetInsuranceAgentsList() {
            return _cache.GetEntity<InsuranceAgent>().Select(e => new SelectListItem() {
                Text = $"{e.Surname} {e.Name} {e.MiddleName}",
                Value = e.Id.ToString()
            });
        }

        private IEnumerable<SelectListItem> GetInsuranceTypesList() {
            return _cache.GetEntity<InsuranceType>().Select(e => new SelectListItem() {
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
            _cache.SetEntity<PolicyClient>();
        }
    }
}
