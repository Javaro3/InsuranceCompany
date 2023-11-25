using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Areas.Identity.Models {
    public class LoginInputModel {
        [Required(ErrorMessage = "Необходимо ввести имя в поле.")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести фамилию в поле.")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Необходимо ввести отчество в поле.")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль в поле.")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
