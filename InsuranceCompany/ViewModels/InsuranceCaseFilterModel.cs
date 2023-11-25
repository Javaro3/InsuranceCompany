using InsuranceCompany.Data.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceCompany.ViewModels {
    public class InsuranceCaseFilterModel {
        [Display(Name = "Подтверждащий документ")]
        public string SupportingDocument { get; set; }

        [Display(Name = "Дата ОТ")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Дата ДО")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Страховая выплата ОТ")]
        public decimal? MinInsurancePayment { get; set; }

        [Display(Name = "Страховая выплата ДО")]
        public decimal? MaxInsurancePayment { get; set; }
        
        [Display(Name = "Тип страховки")]
        public int? InsuranceTypeId { get; set; }

        [Display(Name = "Сортировка по страховой выплате")]
        public SortType SortTypeInsurancePayment { get; set; }

        [Display(Name = "Сортировка по дате")]
        public SortType SortTypeDate { get; set; }

        [Display(Name = "Сортировка по подтверждающему документу")]
        public SortType SortTypeSupportingDocument { get; set; }

        [JsonIgnore]
        public IEnumerable<SelectListItem> InsuranceTypeList { get; set; } = null!;
    }
}
