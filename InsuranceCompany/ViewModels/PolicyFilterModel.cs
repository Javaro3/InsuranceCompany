using InsuranceCompany.Data.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceCompany.ViewModels {
    public class PolicyFilterModel {
        [Display(Name = "Страховой агент")]
        public int? InsuranceAgentId { get; set; }

        [Display(Name = "Тип страховки")]
        public int? InsuranceTypeId { get; set; }

        [MaxLength(16)]
        [Display(Name = "Номер полиса")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Дата ОТ")]
        public DateTime? ApplicationDateStart { get; set; }

        [Display(Name = "Дата ДО")]
        public DateTime? ApplicationDateEnd { get; set; }

        [Display(Name = "Страховая выплата ОТ")]
        public decimal? MinPolicyPayment { get; set; }

        [Display(Name = "Страховая выплата ДО")]
        public decimal? MaxPolicyPayment { get; set; }

        [Display(Name = "Действующие полисы")]
        public bool PolicyIsActing { get; set; }

        [Display(Name = "Сортировка страховой выплате")]
        public SortType SortTypePolicyPaymen { get; set; }

        [Display(Name = "Страховая продолжительности полиса")]
        public SortType SortTypePolicyTerm { get; set; }

        [Display(Name = "Страховая по дате")]
        public SortType SortTypeApplicationDate { get; set; }

        [JsonIgnore]
        public IEnumerable<SelectListItem> InsuranceAgentList { get; set; } = null!;

        [JsonIgnore]
        public IEnumerable<SelectListItem> InsuranceTypeList { get; set; } = null!;
    }
}
