namespace InsuranceCompany.Models;

public partial class PolicyInsuranceCase {
    public int Id { get; set; }

    public int PolicyId { get; set; }

    public int InsuranceCaseId { get; set; }

    public virtual InsuranceCase InsuranceCase { get; set; } = null!;

    public virtual Policy Policy { get; set; } = null!;
}
