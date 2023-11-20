﻿using InsuranceCompany.Data;
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
    [Authorize(Roles = "Клиент")]
    public class ClientInsuranceCasesController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 9;

        public ClientInsuranceCasesController(
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
            var insuranceCaseFilter = _cookieManager.GetCookie<InsuranceCaseFilterModel>(HttpContext.Request.Cookies);
            var client = GetClient();
            var insuranceCases = _filter.Filter(insuranceCaseFilter).Where(e => e.ClientId == client.Id);
            var viewModel = new PageModel<InsuranceCase, InsuranceCaseFilterModel>(page, pageSize, insuranceCases, insuranceCaseFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InsuranceCaseFilterModel insuranceCaseFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(insuranceCaseFilter, HttpContext.Response.Cookies);
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
                _cache.SetEntity<InsuranceCase>();
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
                    _cache.SetEntity<InsuranceCase>();
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
            _cache.SetEntity<InsuranceCase>();
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetSupportingDocumentsList() {
            return _cache.GetEntity<SupportingDocument>()
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
    }
}
