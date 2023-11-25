using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class AgentType {
    public int Id { get; set; }

    [Display(Name = "Тип страхового агента")]
    [Required(ErrorMessage = "Тип страхового агента это обязательное поле")]
    public string Type { get; set; } = null!;

    public virtual ICollection<InsuranceAgent> InsuranceAgents { get; set; } = new List<InsuranceAgent>();
}
