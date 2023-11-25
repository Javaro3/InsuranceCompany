using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models {
    public class ApplicationUser : IdentityUser {
        [MaxLength(50)]
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя это обязательное поле")]
        public string Name { get; set; } = null!;
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия это обязательное поле")]
        public string Surname { get; set; } = null!;
        [MaxLength(50)]
        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Отчество это обязательное поле")]
        public string MiddleName { get; set; } = null!;
    }
}
