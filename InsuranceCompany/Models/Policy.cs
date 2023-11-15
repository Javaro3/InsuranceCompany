using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class Policy {
    public int Id { get; set; }

    public int InsuranceAgentId { get; set; }

    [DataType(DataType.Date)]
    public DateTime ApplicationDate { get; set; }

    [RegularExpression(@"^\d{16}$", ErrorMessage = "Номер полиса должен состоять из 16 цифр")]
    [MaxLength(16, ErrorMessage = "Длина полиса должна быть равна 16")]
    public string PolicyNumber { get; set; } = null!;

    public int InsuranceTypeId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Продолжительность действия полиса должна быть больше 0")]
    public int PolicyTerm { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Страховая выплота должна быть больше 0")]
    public decimal PolicyPayment { get; set; }

    public virtual InsuranceAgent? InsuranceAgent { get; set; } = null!;

    public virtual InsuranceType? InsuranceType { get; set; } = null!;
}
