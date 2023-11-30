using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Service;

namespace InsuranceCompany.Controllers {
    public class HomeController : Controller {
        private readonly InsuranceCompanyCache _cache;

        public HomeController(InsuranceCompanyCache cache) {
            _cache = cache;
        }

        public IActionResult Index() {
            return View(_cache.GetEntity<InsuranceType>());
        }

        public IActionResult Privacy() {
            return View();
        }
    }
}