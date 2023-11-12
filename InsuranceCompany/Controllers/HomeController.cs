using InsuranceCompany.Data;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InsuranceCompany.Controllers {
    public class HomeController : Controller {
        private readonly InsuranceCompanyContext _db;

        public HomeController(InsuranceCompanyContext db) {
            _db = db;
        }

        public IActionResult Index() {
            return View(_db.InsuranceTypes);
        }

        public IActionResult Privacy() {
            return View();
        }
    }
}