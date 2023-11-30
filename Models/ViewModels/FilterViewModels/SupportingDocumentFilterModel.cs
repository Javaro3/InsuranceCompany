using Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.FilterViewModels {
    public class SupportingDocumentFilterModel {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Сортировка по названию")]
        public SortType SortTypeName { get; set; }
    }
}