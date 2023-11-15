using InsuranceCompany.Data.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceCompany.ViewModels {
    public class PolicyFilterModel {
        public int? InsuranceAgentId { get; set; }
        public int? InsuranceTypeId { get; set; }

        [MaxLength(16)]
        public string PolicyNumber { get; set; }

        public DateTime? ApplicationDateStart { get; set; }
        public DateTime? ApplicationDateEnd { get; set; }

        public decimal? MinPolicyPayment { get; set; }
        public decimal? MaxPolicyPayment { get; set; }

        public bool PolicyIsActing { get; set; }

        public SortType SortTypePolicyPaymen { get; set; }

        public SortType SortTypePolicyTerm { get; set; }

        public SortType SortTypeApplicationDate { get; set; }

        [JsonIgnore]
        public IEnumerable<SelectListItem> InsuranceAgentList { get; set; } = null!;

        [JsonIgnore]
        public IEnumerable<SelectListItem> InsuranceTypeList { get; set; } = null!;
    }
}
