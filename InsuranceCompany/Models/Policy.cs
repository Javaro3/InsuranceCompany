namespace InsuranceCompany.Models;

public partial class Policy {
    public int Id { get; set; }

    public int InsuranceAgentId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public string PolicyNumber { get; set; } = null!;

    public int InsuranceTypeId { get; set; }

    public int PolicyTerm { get; set; }

    public decimal PolicyPayment { get; set; }

    public virtual InsuranceAgent InsuranceAgent { get; set; } = null!;

    public virtual InsuranceType InsuranceType { get; set; } = null!;
}
