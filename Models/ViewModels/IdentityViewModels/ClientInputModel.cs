using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.IdentityViewModels {
    public class ClientInputModel {
        [Required(ErrorMessage = "Необходимо ввести дату рождения в поле.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime BrithDate { get; set; }

        [Required(ErrorMessage = "Необходимо ввести номер телефона в поле.")]
        [Display(Name = "Номер телефона")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Необходимо ввести адрес в поле.")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Необходимо ввести номер пасспорта в поле.")]
        [Display(Name = "Номер пасспорта")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "Необходимо ввести дату выдачи пасспорта в поле.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата выдачи пасспорта")]
        public DateTime PassportIssueDate { get; set; }
    }
}
