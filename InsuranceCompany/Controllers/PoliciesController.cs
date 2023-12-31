﻿using InsuranceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.ViewModels.FilterViewModels;
using Models.ViewModels.PageViewModels;
using Repository;
using Service;
using System.Data;

namespace InsuranceCompany.Controllers {
    [Authorize(Roles = "Страховой агент")]
    public class PoliciesController : BaseController, IUpdateCache {
        private const int PAGE_SIZE = 9;

        public PoliciesController(
            InsuranceCompanyContext context,
            InsuranceCompanyCache cache,
            InsuranceCompanyCookieManager cookieManager,
            InsuranceCompanyFilter filter) : base(context, cache, cookieManager, filter) {
        }

        public async Task<IActionResult> Index(int page, int pageSize = PAGE_SIZE) {
            var policyFilter = _cookieManager.GetCookie<PolicyFilterModel>(HttpContext.Request.Cookies);
            policyFilter.InsuranceAgentList = GetInsuranceAgentsList();
            policyFilter.InsuranceTypeList = GetInsuranceTypesList();
            var contracts = _filter.Filter(policyFilter);
            var totalInsurancePayment = contracts.Sum(e => e.PolicyPayment);
            var viewModel = new PageModelWithAggregateValue<Policy, PolicyFilterModel, decimal>(page, pageSize, contracts, policyFilter, totalInsurancePayment);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PolicyFilterModel policyFilter, int page, int pageSize = PAGE_SIZE) {
            _cookieManager.SetCookie(policyFilter, HttpContext.Response.Cookies);
            policyFilter.InsuranceAgentList = GetInsuranceAgentsList();
            policyFilter.InsuranceTypeList = GetInsuranceTypesList();
            var contracts = _filter.Filter(policyFilter);
            var totalInsurancePayment = contracts.Sum(e => e.PolicyPayment);
            var viewModel = new PageModelWithAggregateValue<Policy, PolicyFilterModel, decimal>(page, pageSize, contracts, policyFilter, totalInsurancePayment);

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

            var clients = _cache.GetEntity<PolicyClient>().Where(e => e.PolicyId == id).Select(e => e.Client);
            var viewModel = (policy, clients);

            return View(viewModel);
        }

        public IActionResult Create() {
            ViewData["InsuranceAgentId"] = GetInsuranceAgentsList();
            ViewData["InsuranceTypeId"] = GetInsuranceTypesList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Policy policy) {
            if (ModelState.IsValid) {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                UpdateCache();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceAgentId"] = GetInsuranceAgentsList();
            ViewData["InsuranceTypeId"] = GetInsuranceTypesList();
            return View(policy);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var policy = _cache.GetEntity<Policy>().FirstOrDefault(m => m.Id == id);
            if (policy == null) {
                return NotFound();
            }
            ViewData["InsuranceAgentId"] = GetInsuranceAgentsList();
            ViewData["InsuranceTypeId"] = GetInsuranceTypesList();
            return View(policy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Policy policy) {
            if (id != policy.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                    UpdateCache();
                }
                catch (DbUpdateConcurrencyException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceAgentId"] = GetInsuranceAgentsList();
            ViewData["InsuranceTypeId"] = GetInsuranceTypesList();
            return View(policy);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var policy = _cache.GetEntity<Policy>().FirstOrDefault(m => m.Id == id);
            if (policy != null) {
                _context.Policies.Remove(policy);
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

        public void UpdateCache() {
            _cache.SetEntity<Policy>();
            _cache.SetEntity<PolicyClient>();
        }
    }
}