using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class InsuranceAgentsController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 9;

        public InsuranceAgentsController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter,
            UserManager<ApplicationUser> userManager) {
            _context = context;
            _cache = cache;
            _cookieManager = cookieManager;
            _filter = filter;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page, int pageSize = PAGE_SIZE) {
            var insuranceAgentFilter = _cookieManager.GetCookie<InsuranceAgentFilterModel>(HttpContext.Request.Cookies);
            var insuranceAgents = _filter.Filter(insuranceAgentFilter);
            var viewModel = new PageModel<InsuranceAgent, InsuranceAgentFilterModel>(page, pageSize, insuranceAgents, insuranceAgentFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InsuranceAgentFilterModel insuranceAgentFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(insuranceAgentFilter, HttpContext.Response.Cookies);
            var insuranceAgents = _filter.Filter(insuranceAgentFilter);
            var viewModel = new PageModel<InsuranceAgent, InsuranceAgentFilterModel>(page, pageSize, insuranceAgents, insuranceAgentFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceAgent = _cache.GetEntity<InsuranceAgent>().FirstOrDefault(m => m.Id == id);
            if (insuranceAgent == null) {
                return NotFound();
            }
            insuranceAgent.Policies = _cache.GetEntity<Policy>().Where(e => e.InsuranceAgentId == id).ToList();
            return View(insuranceAgent);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceAgent = _cache.GetEntity<InsuranceAgent>().FirstOrDefault(m => m.Id == id);
            if (insuranceAgent == null) {
                return NotFound();
            }
            ViewData["AgentTypeId"] = GetAgentTypesList();
            ViewData["ContractId"] = GetContractsList();
            return View(insuranceAgent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceAgent insuranceAgent) {
            if (id != insuranceAgent.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    var oldInsuranceAgent = _cache.GetEntity<InsuranceAgent>().FirstOrDefault(e => e.Id == id);

                    var oldUserName = DbConverter.GetUserNameTranslator(oldInsuranceAgent);
                    var newUserName = DbConverter.GetUserNameTranslator(insuranceAgent);

                    var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == oldUserName);
                    applicationUser.Name = insuranceAgent.Name;
                    applicationUser.Surname = insuranceAgent.Surname;
                    applicationUser.MiddleName = insuranceAgent.MiddleName;
                    applicationUser.UserName = newUserName;

                    await _userManager.UpdateAsync(applicationUser);
                    _context.Update(insuranceAgent);

                    await _context.SaveChangesAsync();
                    UpdateCache();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentTypeId"] = GetAgentTypesList();
            ViewData["ContractId"] = GetContractsList();
            return View(insuranceAgent);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var insuranceAgent = _cache.GetEntity<InsuranceAgent>().FirstOrDefault(m => m.Id == id);
            if (insuranceAgent == null) {
                return NotFound();
            }

            return View(insuranceAgent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var insuranceAgent = _cache.GetEntity<InsuranceAgent>().FirstOrDefault(m => m.Id == id);
            if (insuranceAgent != null) {
                _context.InsuranceAgents.Remove(insuranceAgent);
                var userName = DbConverter.GetUserNameTranslator(insuranceAgent);
                var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == userName);
                await _userManager.DeleteAsync(applicationUser);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetContractsList() {
            return _cache.GetEntity<Contract>().Select(e => new SelectListItem() {
                Text = $"{e.Responsibilities} ({e.StartDeadline.ToShortDateString()} - {e.EndDeadline.ToShortDateString()})",
                Value = e.Id.ToString()
            });
        }

        private IEnumerable<SelectListItem> GetAgentTypesList() {
            return _cache.GetEntity<AgentType>().Select(e => new SelectListItem() {
                Text = e.Type,
                Value = e.Id.ToString()
            });
        }

        private void UpdateCache() {
            _cache.SetEntity<InsuranceAgent>();
            _cache.SetEntity<Policy>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}