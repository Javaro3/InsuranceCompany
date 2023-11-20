using InsuranceCompany.Areas.Identity.Models;
using InsuranceCompany.Data;
using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceCompany.Areas.Identity.Pages.Account {
    public class RegisterModel : PageModel {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly InsuranceCompanyContext _db;
        
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            InsuranceCompanyContext db) {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }
        [BindProperty]
        public ClientInputModel ClientInput { get; set; }
        [BindProperty]
        public InsuranceAgentInputModel InsuranceAgentInput { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null) {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Input = new RegisterInputModel() { RoleList = GetRoleList() };
            InsuranceAgentInput = new InsuranceAgentInputModel() {
                AgentTypeList = GetAgentTypeList(),
                ContractList = GetContractList()
            };
            ClientInput = new ClientInputModel() { BrithDate = DateTime.Now, PassportIssueDate = DateTime.Now };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if ((Input.Role == "Клиент" && ModelState.ErrorCount <= 2) || (Input.Role == "Страховой агент" && ModelState.ErrorCount <= 3)) {
                var user = CreateUser();
                var userName = DbConverter.GetTranslator($"{Input.Name.Trim()}_{Input.Surname.Trim()}_{Input.MiddleName.Trim()}");

                await _userStore.SetUserNameAsync(user, userName, CancellationToken.None);

                user.Name = Input.Name;
                user.Surname = Input.Surname;
                user.MiddleName = Input.MiddleName;

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    if (Input.Role == "Клиент")
                        AddClient();
                    else if (Input.Role == "Страховой агент")
                        AddInsuranceAgent();

                    _db.SaveChanges();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            Input.RoleList = GetRoleList();
            InsuranceAgentInput.ContractList = GetContractList();
            InsuranceAgentInput.AgentTypeList = GetAgentTypeList();
            return Page();
        }

        private void AddClient() {
            _db.Add(new Client() {
                Name = Input.Name,
                Surname = Input.Surname,
                MiddleName = Input.MiddleName,
                Birthdate = ClientInput.BrithDate,
                Address = ClientInput.Address,
                MobilePhone = ClientInput.MobilePhone,
                PassportNumber = ClientInput.PassportNumber,
                PassportIssueDate = ClientInput.PassportIssueDate
            });
        }

        private void AddInsuranceAgent() {
            _db.Add(new InsuranceAgent() {
                Name = Input.Name,
                Surname = Input.Surname,
                MiddleName = Input.MiddleName,
                AgentTypeId = int.Parse(InsuranceAgentInput.AgentType),
                ContractId = int.Parse(InsuranceAgentInput.Contract)
            });
        }

        private IEnumerable<SelectListItem> GetRoleList() {
            return _roleManager.Roles.Select(e => new SelectListItem() { Text = e.Name, Value = e.Name });
        }

        private IEnumerable<SelectListItem> GetAgentTypeList() {
            return _db.AgentTypes.Select(e => new SelectListItem() { Text = e.Type, Value = e.Id.ToString() }).ToList();
        }

        private IEnumerable<SelectListItem> GetContractList() {
            return _db.Contracts.Select(e => new SelectListItem() { Text = $"{e.Responsibilities}\n({e.StartDeadline.ToShortDateString()} - {e.EndDeadline.ToShortDateString()})", Value = e.Id.ToString() }).ToList();
        }

        private ApplicationUser CreateUser() {
            try {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
