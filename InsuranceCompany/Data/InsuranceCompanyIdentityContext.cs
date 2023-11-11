using InsuranceCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Data {
    public partial class InsuranceCompanyIdentityContext : IdentityDbContext<IdentityUser> {
        public InsuranceCompanyIdentityContext(DbContextOptions options) : base(options) {
        }

        protected InsuranceCompanyIdentityContext() {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
