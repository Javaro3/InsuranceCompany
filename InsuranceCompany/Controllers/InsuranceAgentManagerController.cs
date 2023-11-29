using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Utilities;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class InsuranceAgentManagerController : Controller, IUpdateCache {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly UserManager<ApplicationUser> _userManager;

        public InsuranceAgentManagerController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            UserManager<ApplicationUser> userManager) {
            _context = context;
            _cache = cache;
            _userManager = userManager;
        }

        public IActionResult Index() {
            ViewData["AgentTypeId"] = GetAgentTypesList();
            ViewData["ContractId"] = GetContractsList();
            var insuranceAgent = GetInsuranceAgent();
            insuranceAgent.Policies = _cache
                .GetEntity<Policy>()
                .Where(e => e.InsuranceAgentId == insuranceAgent.Id).ToList();
            var clients = _cache
                .GetEntity<PolicyClient>()
                .Where(e => e.Policy.InsuranceAgentId == insuranceAgent.Id)
                .Select(e => e.Client);
            return View((insuranceAgent, clients));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InsuranceAgent insuranceAgent) {
            if (ModelState.IsValid) {
                try {
                    var oldInsuranceAgent = GetInsuranceAgent();

                    var oldUserName = $"{oldInsuranceAgent.Name} {oldInsuranceAgent.Surname} {oldInsuranceAgent.MiddleName}";
                    var newUserName = $"{insuranceAgent.Name} {insuranceAgent.Surname} {insuranceAgent.MiddleName}";

                    var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == oldUserName);
                    applicationUser.Name = insuranceAgent.Name;
                    applicationUser.Surname = insuranceAgent.Surname;
                    applicationUser.MiddleName = insuranceAgent.MiddleName;
                    applicationUser.UserName = newUserName;
                    insuranceAgent.Id = oldInsuranceAgent.Id;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            return RedirectToAction(nameof(Index));
        }

        private InsuranceAgent GetInsuranceAgent() {
            var applicationUser = _userManager.GetUserAsync(HttpContext.User).Result;
            return _cache.GetEntity<InsuranceAgent>()
                .FirstOrDefault(e =>
                    applicationUser.Name == e.Name &&
                    applicationUser.Surname == e.Surname &&
                    applicationUser.MiddleName == e.MiddleName);
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

        public void UpdateCache() {
            _cache.SetEntity<InsuranceAgent>();
            _cache.SetEntity<Policy>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}
