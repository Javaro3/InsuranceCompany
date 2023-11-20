using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models {
    public class ApplicationUser : IdentityUser {
        [MaxLength(50)]
        [Display(Name = "Имя")]
        public string Name { get; set; } = null!;
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; } = null!;
        [MaxLength(50)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; } = null!;
    }
}
