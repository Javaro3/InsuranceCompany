using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public partial class Client {
    public int Id { get; set; }

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Имя это обязательное поле")]
    public string Name { get; set; } = null!;

    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Фамилия это обязательное поле")]
    public string Surname { get; set; } = null!;

    [Display(Name = "Отчество")]
    [Required(ErrorMessage = "Отчество это обязательное поле")]
    public string MiddleName { get; set; } = null!;

    [Display(Name = "Дата рождения")]
    [Required(ErrorMessage = "Дата рождения это обязательное поле")]
    public DateTime Birthdate { get; set; }

    [Display(Name = "Номер телефона")]
    [Required(ErrorMessage = "Номер телефона это обязательное поле")]
    public string MobilePhone { get; set; } = null!;

    [Display(Name = "Адрес")]
    [Required(ErrorMessage = "Адрес это обязательное поле")]
    public string Address { get; set; } = null!;

    [Display(Name = "Номер пасспорта")]
    [Required(ErrorMessage = "Номер пасспорта это обязательное поле")]
    public string PassportNumber { get; set; } = null!;

    [Display(Name = "Дата выдачи пасспорта")]
    [Required(ErrorMessage = "Дата выдачи пасспорта это обязательное поле")]
    public DateTime PassportIssueDate { get; set; }

    public virtual ICollection<InsuranceCase> InsuranceCases { get; set; } = new List<InsuranceCase>();
}