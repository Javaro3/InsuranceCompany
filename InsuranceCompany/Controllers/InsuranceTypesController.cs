﻿using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Controllers {
    public class InsuranceTypesController : Controller {
        private readonly InsuranceCompanyContext _context;
        private readonly InsuranceCompanyCache _cache;
        private readonly InsuranceCompanyCookieManager _cookieManager;
        private readonly InsuranceCompanyFilter _filter;
        private const int PAGE_SIZE = 10;

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
            var insuranceTypeFilter = _cookieManager.GetCookie<InsuranceTypeFilter>(HttpContext.Request.Cookies);
            ViewData["InsuranceTypeFilter"] = insuranceTypeFilter;
            var insuranceTypes = _filter.Filter(insuranceTypeFilter);
            var viewModel = new PageModel<InsuranceType>(page, pageSize, insuranceTypes);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InsuranceTypeFilter insuranceTypeFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(insuranceTypeFilter, HttpContext.Response.Cookies);
            ViewData["InsuranceTypeFilter"] = insuranceTypeFilter;
            var insuranceTypes = _filter.Filter(insuranceTypeFilter);
            var viewModel = new PageModel<InsuranceType>(page, pageSize, insuranceTypes);

            return View(viewModel);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] InsuranceType insuranceType) {
            if (ModelState.IsValid) {
                _context.Add(insuranceType);
                await _context.SaveChangesAsync();
                _cache.SetEntity<InsuranceType>();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] InsuranceType insuranceType) {
            if (id != insuranceType.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(insuranceType);
                    await _context.SaveChangesAsync();
                    _cache.SetEntity<InsuranceType>();
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
            _cache.SetEntity<InsuranceType>();
            return RedirectToAction(nameof(Index));
        }
    }
}