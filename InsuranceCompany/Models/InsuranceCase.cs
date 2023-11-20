using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models;

public partial class InsuranceCase {
    public int Id { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    [Display(Name = "Подтверждающие документы")]
    public int SupportingDocumentId { get; set; }

    [Display(Name = "Страховая выплота")]
    public decimal InsurancePayment { get; set; }

    [Display(Name = "Клиент")]
    public int ClientId { get; set; }

    public virtual SupportingDocument? SupportingDocument { get; set; } = null!;

    public virtual Client? Client { get; set; } = null!;
}
