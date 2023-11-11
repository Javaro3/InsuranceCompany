using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Areas.Identity.Models {
    public class InsuranceAgentInputModel {
        [Required(ErrorMessage = "Необходимо выбрать тип агента из списка")]
        [Display(Name = "Тип страхового агента")]
        public string? AgentType { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AgentTypeList { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать контракт из списка")]
        [Display(Name = "Контракты")]
        public string? Contract { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ContractList { get; set; }
    }
}
