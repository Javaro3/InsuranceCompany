using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class SupportingDocument {
    public int Id { get; set; }

    [Display(Name = "Название")]
    [Required(ErrorMessage = "Название это обязательное поле")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "Описание это обязательное поле")]
    public string? Description { get; set; }

    public virtual ICollection<InsuranceCase> InsuranceCases { get; set; } = new List<InsuranceCase>();
}
