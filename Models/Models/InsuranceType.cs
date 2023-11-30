using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public partial class InsuranceType {
    public int Id { get; set; }

    [Display(Name = "Тип страховки")]
    [Required(ErrorMessage = "Тип страховки это обязательное поле")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "Описание это обязательное поле")]
    public string? Description { get; set; }

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
