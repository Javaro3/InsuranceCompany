using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models {
    public class ApplicationUser : IdentityUser {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(50)]
        public string Surname { get; set; } = null!;
        [MaxLength(50)]
        public string MiddleName { get; set; } = null!;
    }
}
