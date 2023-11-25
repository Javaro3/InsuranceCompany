using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Клиент")]
    public class ClientManagerController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientManagerController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            UserManager<ApplicationUser> userManager) {
            _context = context;
            _cache = cache;
            _userManager = userManager;
        }

        public IActionResult Index() {
            var client = GetClient();
            client.InsuranceCases = _cache
                .GetEntity<InsuranceCase>()
                .Where(e => e.ClientId == client.Id).ToList();
            var policies = _cache
                .GetEntity<PolicyClient>()
                .Where(e => e.ClientId == client.Id)
                .Select(e => e.Policy);
            return View((client, policies));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client) {
            if (ModelState.IsValid) {
                try {
                    var oldClient = GetClient();

                    var oldUserName = $"{oldClient.Name} {oldClient.Surname} {oldClient.MiddleName}";
                    var newUserName = $"{client.Name} {client.Surname} {client.MiddleName}";

                    var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == oldUserName);
                    applicationUser.Name = client.Name;
                    applicationUser.Surname = client.Surname;
                    applicationUser.MiddleName = client.MiddleName;
                    applicationUser.UserName = newUserName;
                    client.Id = oldClient.Id;

                    await _userManager.UpdateAsync(applicationUser);
                    _context.Update(client);

                    await _context.SaveChangesAsync();
                    UpdateCache();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            return RedirectToAction(nameof(Index));
        }

        private Client GetClient() {
            var applicationUser = _userManager.GetUserAsync(HttpContext.User).Result;
            return _cache.GetEntity<Client>()
                .FirstOrDefault(e =>
                    applicationUser.Name == e.Name &&
                    applicationUser.Surname == e.Surname &&
                    applicationUser.MiddleName == e.MiddleName);
        }

        private void UpdateCache() {
            _cache.SetEntity<Client>();
            _cache.SetEntity<InsuranceCase>();
            _cache.SetEntity<SupportingDocument>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}
