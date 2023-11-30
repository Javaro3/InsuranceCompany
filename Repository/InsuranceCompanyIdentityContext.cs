using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Repository {
    public partial class InsuranceCompanyIdentityContext : IdentityDbContext<IdentityUser> {
        public InsuranceCompanyIdentityContext(DbContextOptions options) : base(options) {}
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}