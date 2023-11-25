using InsuranceCompany.Data.Utilities;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.ViewModels {
    public class SupportingDocumentFilterModel {
        [Display(Name = "Название")]
        public string Name { get;set; }

        [Display(Name = "Сортировка по названию")]
        public SortType SortTypeName { get; set; }
    }
}