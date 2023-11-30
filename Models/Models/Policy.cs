using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public partial class Policy {
    public int Id { get; set; }

    [Display(Name = "Страховой агент")]
    [Required(ErrorMessage = "Страховой агент это обязательное поле")]
    public int InsuranceAgentId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата начала полиса")]
    [Required(ErrorMessage = "Дата начала полиса это обязательное поле")]
    public DateTime ApplicationDate { get; set; }

    [RegularExpression(@"^\d{16}$", ErrorMessage = "Номер полиса должен состоять из 16 цифр")]
    [MaxLength(16, ErrorMessage = "Длина полиса должна быть равна 16")]
    [Display(Name = "Номер полиса")]
    [Required(ErrorMessage = "Номер полиса это обязательное поле")]
    public string PolicyNumber { get; set; } = null!;

    [Display(Name = "Тип страховки")]
    [Required(ErrorMessage = "Тип страховки это обязательное поле")]
    public int InsuranceTypeId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Продолжительность действия полиса должна быть больше 0")]
    [Display(Name = "Продолжительность полиса")]
    [Required(ErrorMessage = "Продолжительность полиса это обязательное поле")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Продолжитеность должна быть целым числом")]
    public int PolicyTerm { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Страховая выплота должна быть больше 0")]
    [Display(Name = "Страховая выплота")]
    [Required(ErrorMessage = "Страховая выплота это обязательное поле")]
    public decimal PolicyPayment { get; set; }

    public virtual InsuranceAgent? InsuranceAgent { get; set; } = null!;

    public virtual InsuranceType? InsuranceType { get; set; } = null!;
}
