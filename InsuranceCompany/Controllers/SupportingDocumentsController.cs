﻿using InsuranceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.ViewModels.FilterViewModels;
using Models.ViewModels.PageViewModels;
using Repository;
using Service;
using System.Data;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class SupportingDocumentsController : BaseController, IUpdateCache {
        private const int PAGE_SIZE = 9;

        public SupportingDocumentsController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter) : base(context, cache, cookieManager, filter) {
        }

        public async Task<IActionResult> Index(int page, int pageSize = PAGE_SIZE) {
            var supportingDocumentFilter = _cookieManager.GetCookie<SupportingDocumentFilterModel>(HttpContext.Request.Cookies);
            var supportingDocuments = _filter.Filter(supportingDocumentFilter);
            var viewModel = new PageModel<SupportingDocument, SupportingDocumentFilterModel>(page, pageSize, supportingDocuments, supportingDocumentFilter);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SupportingDocumentFilterModel supportingDocumentFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(supportingDocumentFilter, HttpContext.Response.Cookies);
            var supportingDocuments = _filter.Filter(supportingDocumentFilter);
            var viewModel = new PageModel<SupportingDocument, SupportingDocumentFilterModel>(page, pageSize, supportingDocuments, supportingDocumentFilter);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var supportingDocument = _cache.GetEntity<SupportingDocument>().FirstOrDefault(e => e.Id == id);
            if (supportingDocument == null) {
                return NotFound();
            }
            supportingDocument.InsuranceCases = _cache.GetEntity<InsuranceCase>().Where(e => e.SupportingDocumentId == id).ToList();
            return View(supportingDocument);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupportingDocument supportingDocument) {
            if (ModelState.IsValid) {
                _context.Add(supportingDocument);
                await _context.SaveChangesAsync();
                UpdateCache();
                return RedirectToAction(nameof(Index));
            }
            return View(supportingDocument);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var supportingDocument = _cache.GetEntity<SupportingDocument>().FirstOrDefault(e => e.Id == id);
            if (supportingDocument == null) {
                return NotFound();
            }
            return View(supportingDocument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupportingDocument supportingDocument) {
            if (id != supportingDocument.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(supportingDocument);
                    UpdateCache();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(supportingDocument);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var supportingDocument = _cache.GetEntity<SupportingDocument>().FirstOrDefault(e => e.Id == id);
            if (supportingDocument == null) {
                return NotFound();
            }
            supportingDocument.InsuranceCases = _cache.GetEntity<InsuranceCase>().Where(e => e.SupportingDocumentId == id).ToList();

            return View(supportingDocument);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var supportingDocument = _cache.GetEntity<SupportingDocument>().FirstOrDefault(e => e.Id == id);

            if (supportingDocument != null) {
                _context.SupportingDocuments.Remove(supportingDocument);
            }

            await _context.SaveChangesAsync();
            UpdateCache();
            return RedirectToAction(nameof(Index));
        }

        public void UpdateCache() {
            _cache.SetEntity<SupportingDocument>();
            _cache.SetEntity<InsuranceCase>();
        }
    }
}