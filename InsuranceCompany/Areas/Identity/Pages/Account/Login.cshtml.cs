using InsuranceCompany.Areas.Identity.Models;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceCompany.Areas.Identity.Pages.Account {
    public class LoginModel : PageModel {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) {
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null) {
            if (!string.IsNullOrEmpty(ErrorMessage)) {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
            Input = new InputModel() { RoleList = GetRoleList() };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var role = Input.Role == "Клиент" ? "Client" : "InsuranceAgent";
            var userName = DbConverter.GetUserNameTranslator($"{Input.Name.Trim()}_{Input.Surname.Trim()}_{Input.MiddleName.Trim()}_{role}");

            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded) {
                    return LocalRedirect(returnUrl);
                }
                else {
                    ModelState.AddModelError(string.Empty, "Неверная попытка входа в систему.");
                    Input.RoleList = GetRoleList();
                    return Page();
                }
            }

            Input.RoleList = GetRoleList();
            return Page();
        }

        private IEnumerable<SelectListItem> GetRoleList() {
            return _roleManager.Roles.Select(e => new SelectListItem() { Text = e.Name, Value = e.Name });
        }
    }
}
