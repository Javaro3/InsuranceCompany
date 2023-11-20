using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class AgentType {
    public int Id { get; set; }

    [Display(Name = "Тип страхового агента")]
    public string Type { get; set; } = null!;

    public virtual ICollection<InsuranceAgent> InsuranceAgents { get; set; } = new List<InsuranceAgent>();
}
