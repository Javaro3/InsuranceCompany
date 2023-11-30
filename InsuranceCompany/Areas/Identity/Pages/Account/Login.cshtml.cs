using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Models.ViewModels.IdentityViewModels;

namespace InsuranceCompany.Areas.Identity.Pages.Account {
    public class LoginModel : PageModel {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

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
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var userName = $"{Input.Name.Trim()} {Input.Surname.Trim()} {Input.MiddleName.Trim()}";

            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded) {
                    return LocalRedirect(returnUrl);
                }
                else {
                    ModelState.AddModelError(string.Empty, "Неверная попытка входа в систему.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
