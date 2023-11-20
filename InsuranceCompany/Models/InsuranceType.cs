using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class InsuranceType {
    public int Id { get; set; }

    [Display(Name = "Тип страховки")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
