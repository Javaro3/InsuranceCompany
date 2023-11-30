using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.IdentityViewModels {
    public class ChangePasswordViewModel {
        [Required(ErrorMessage = "Необходимо ввести старый пароль в поле.")]
        [Display(Name = "Старый Пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Необходимо ввести новый пароль в поле.")]
        [Display(Name = "Новый Пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
