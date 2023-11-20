using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class Client {
    public int Id { get; set; }

    [Display(Name = "Имя")]
    public string Name { get; set; } = null!;

    [Display(Name = "Фамилия")] 
    public string Surname { get; set; } = null!;

    [Display(Name = "Отчество")]
    public string MiddleName { get; set; } = null!;

    [Display(Name = "Дата рождения")]
    public DateTime Birthdate { get; set; }

    [Display(Name = "Номер телефона")]
    public string MobilePhone { get; set; } = null!;

    [Display(Name = "Адрес")]
    public string Address { get; set; } = null!;

    [Display(Name = "Номер пасспорта")]
    public string PassportNumber { get; set; } = null!;

    [Display(Name = "Дата выдачи пасспорта")]
    public DateTime PassportIssueDate { get; set; }

    public virtual ICollection<InsuranceCase> InsuranceCases { get; set; } = new List<InsuranceCase>();
}