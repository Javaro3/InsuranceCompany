using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class Contract {
    public int Id { get; set; }

    public string Responsibilities { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime StartDeadline { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndDeadline { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Зарплата должна быть больше 0")]
    public decimal Salary { get; set; }

    [Range(0, 1, ErrorMessage = "Процент должен быть в интервале от 0 до 1")]
    public double TransactionPercent { get; set; }

    public virtual ICollection<InsuranceAgent> InsuranceAgents { get; set; } = new List<InsuranceAgent>();

}
