using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class InsuranceCase {
    public int Id { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Дата это обязательное поле")]
    public DateTime Date { get; set; }

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "Описание это обязательное поле")]
    public string? Description { get; set; }

    [Display(Name = "Подтверждающие документы")]
    [Required(ErrorMessage = "Подтверждающие документы это обязательное поле")]
    public int SupportingDocumentId { get; set; }

    [Display(Name = "Страховая выплота")]
    [Required(ErrorMessage = "Страховая выплота это обязательное поле")]
    public decimal InsurancePayment { get; set; }

    [Display(Name = "Клиент")]
    [Required(ErrorMessage = "Клиент это обязательное поле")]
    public int ClientId { get; set; }

    public virtual SupportingDocument? SupportingDocument { get; set; } = null!;

    public virtual Client? Client { get; set; } = null!;
}
