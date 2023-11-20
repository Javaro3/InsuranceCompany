using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class SupportingDocument {
    public int Id { get; set; }

    [Display(Name = "Название")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    public virtual ICollection<InsuranceCase> InsuranceCases { get; set; } = new List<InsuranceCase>();
}
