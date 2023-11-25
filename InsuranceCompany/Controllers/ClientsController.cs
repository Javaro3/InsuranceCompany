using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class ClientsController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 9;

        public ClientsController(
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
            var clientFilter = _cookieManager.GetCookie<ClientFilterModel>(HttpContext.Request.Cookies);
            var clients = _filter.Filter(clientFilter);
            var viewModel = new PageModel<Client, ClientFilterModel>(page, pageSize, clients, clientFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ClientFilterModel clientFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(clientFilter, HttpContext.Response.Cookies);
            var clients = _filter.Filter(clientFilter);
            var viewModel = new PageModel<Client, ClientFilterModel>(page, pageSize, clients, clientFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var client = _cache.GetEntity<Client>().FirstOrDefault(m => m.Id == id);
            if (client == null) {
                return NotFound();
            }

            client.InsuranceCases = _cache.GetEntity<InsuranceCase>().Where(m => m.ClientId == id).ToList();
            var policies = _cache.GetEntity<PolicyClient>().Where(e => e.ClientId == id).Select(e => e.Policy);

            var viewModel = (client, policies);

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var client = _cache.GetEntity<Client>().FirstOrDefault(m => m.Id == id);
            if (client == null) {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client) {
            if (id != client.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    var oldClient = _cache.GetEntity<Client>().FirstOrDefault(e => e.Id == id);

                    var oldUserName = DbConverter.GetUserNameTranslator(oldClient);
                    var newUserName = DbConverter.GetUserNameTranslator(client);

                    var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == oldUserName);
                    applicationUser.Name = client.Name;
                    applicationUser.Surname = client.Surname;
                    applicationUser.MiddleName = client.MiddleName;
                    applicationUser.UserName = newUserName;

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

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var client = _cache.GetEntity<Client>().FirstOrDefault(m => m.Id == id);
            if (client == null) {
                return NotFound();
            }

            client.InsuranceCases = _cache.GetEntity<InsuranceCase>().Where(e => e.ClientId == id).ToList();
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var client = _cache.GetEntity<Client>().FirstOrDefault(m => m.Id == id);
            if (client != null) {
                _context.Clients.Remove(client);
                var userName = DbConverter.GetUserNameTranslator(client);
                var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == userName);
                await _userManager.DeleteAsync(applicationUser);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        private void UpdateCache() {
            _cache.SetEntity<Client>();
            _cache.SetEntity<InsuranceCase>();
            _cache.SetEntity<SupportingDocument>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}
