using Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.FilterViewModels {
    public class ContractFilterModel {
        [Display(Name = "Обязанности")]
        public string Responsibilities { get; set; } = null!;

        [Display(Name = "Срок действия контракта ОТ")]
        public DateTime? StartDeadLine { get; set; }

        [Display(Name = "Срок действия контракта ДО")]
        public DateTime? EndDeadLine { get; set; }

        [Display(Name = "Зарплата ОТ")]
        public decimal? MinSalary { get; set; }

        [Display(Name = "Зарплата ДО")]
        public decimal? MaxSalary { get; set; }

        [Display(Name = "Процент от сделки ОТ")]
        public double? MinTransactionPercent { get; set; }

        [Display(Name = "Процент от сделки ДО")]
        public double? MaxTransactionPercent { get; set; }

        [Display(Name = "Сортировка по обязанностям")]
        public SortType SortTypeResponsobilities { get; set; }

        [Display(Name = "Сортировка по зарпалате")]
        public SortType SortTypeSalary { get; set; }

        [Display(Name = "Сортировка по проценту от сделки")]
        public SortType SortTypeTransactionPercent { get; set; }

        [Display(Name = "Сортировка по продолжительности контракта")]
        public SortType SortTypeContractDuration { get; set; }
    }
}
