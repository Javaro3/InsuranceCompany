using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class Policy {
    public int Id { get; set; }

    [Display(Name = "Страховой агент")]
    public int InsuranceAgentId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата начала полиса")]
    public DateTime ApplicationDate { get; set; }

    [RegularExpression(@"^\d{16}$", ErrorMessage = "Номер полиса должен состоять из 16 цифр")]
    [MaxLength(16, ErrorMessage = "Длина полиса должна быть равна 16")]
    [Display(Name = "Номер полиса")]
    public string PolicyNumber { get; set; } = null!;

    [Display(Name = "Тип страховки")]
    public int InsuranceTypeId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Продолжительность действия полиса должна быть больше 0")]
    [Display(Name = "Продолжительность полиса")]
    public int PolicyTerm { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Страховая выплота должна быть больше 0")]
    [Display(Name = "Страховая выплота")]
    public decimal PolicyPayment { get; set; }

    public virtual InsuranceAgent? InsuranceAgent { get; set; } = null!;

    public virtual InsuranceType? InsuranceType { get; set; } = null!;
}
