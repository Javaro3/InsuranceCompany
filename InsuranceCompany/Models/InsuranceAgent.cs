using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class InsuranceAgent {
    public int Id { get; set; }

    [Display(Name = "Имя")]
    public string Name { get; set; } = null!;

    [Display(Name = "Фамилия")]
    public string Surname { get; set; } = null!;

    [Display(Name = "Отчество")]
    public string MiddleName { get; set; } = null!;

    [Display(Name = "Тип страхового агента")]
    public int AgentTypeId { get; set; }

    [Display(Name = "Контракт")]
    public int ContractId { get; set; }

    public virtual AgentType? AgentType { get; set; } = null!;

    public virtual Contract? Contract { get; set; } = null!;

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
