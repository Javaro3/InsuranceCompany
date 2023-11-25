using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class Contract {
    public int Id { get; set; }

    [Display(Name = "Обязанности")]
    [Required(ErrorMessage = "Обязанности это обязательное поле")]
    public string Responsibilities { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Начало контракта")]
    [Required(ErrorMessage = "Начало контракта это обязательное поле")]
    public DateTime StartDeadline { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Конец контракта")]
    [Required(ErrorMessage = "Конец контракта это обязательное поле")]
    public DateTime EndDeadline { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Зарплата должна быть больше 0")]
    [Display(Name = "Зарплата")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Поле Зарплата должно быть числом")]
    [Required(ErrorMessage = "Зарплата это обязательное поле")]
    public decimal Salary { get; set; }

    [Range(0, 1, ErrorMessage = "Процент должен быть в интервале от 0 до 1")]
    [Display(Name = "Процент от сделки")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Поле Процент от сделки должно быть числом")]
    [Required(ErrorMessage = "Процент от сделки это обязательное поле")]
    public double TransactionPercent { get; set; }

    public virtual ICollection<InsuranceAgent> InsuranceAgents { get; set; } = new List<InsuranceAgent>();
}
