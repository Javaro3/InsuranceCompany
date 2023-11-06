namespace InsuranceCompany.Models;

public partial class InsuranceCase {
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public int SupportingDocumentId { get; set; }

    public decimal InsurancePayment { get; set; }

    public int ClientId { get; set; }

    public virtual SupportingDocument SupportingDocument { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
