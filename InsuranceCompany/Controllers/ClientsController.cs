﻿using InsuranceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.ViewModels.FilterViewModels;
using Models.ViewModels.PageViewModels;
using Repository;
using Service;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class ClientsController : BaseController, IUpdateCache {
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 9;

        public ClientsController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter,
            UserManager<ApplicationUser> userManager) : base(context, cache, cookieManager, filter) {
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

                    var oldUserName = $"{oldClient.Name} {oldClient.Surname} {oldClient.MiddleName}";
                    var newUserName = $"{client.Name} {client.Surname} {client.MiddleName}";

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
                var userName = $"{client.Name} {client.Surname} {client.MiddleName}";
                var applicationUser = _userManager.Users.FirstOrDefault(e => e.UserName == userName);
                await _userManager.DeleteAsync(applicationUser);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        public void UpdateCache() {
            _cache.SetEntity<Client>();
            _cache.SetEntity<InsuranceCase>();
            _cache.SetEntity<SupportingDocument>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}
