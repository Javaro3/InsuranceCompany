using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public partial class InsuranceAgent {
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

    [Display(Name = "Тип страхового агента")]
    [Required(ErrorMessage = "Тип страхового агента это обязательное поле")]
    public int AgentTypeId { get; set; }

    [Display(Name = "Контракт")]
    [Required(ErrorMessage = "Контракт это обязательное поле")]
    public int ContractId { get; set; }

    public virtual AgentType? AgentType { get; set; } = null!;

    public virtual Contract? Contract { get; set; } = null!;

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
