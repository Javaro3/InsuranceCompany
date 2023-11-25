using InsuranceCompany.Data.Utilities;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.ViewModels {
    public class InsuranceAgentFilterModel {
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Тип страхового агента")]
        public string AgentType { get; set; }

        [Display(Name = "Обязанности")]
        public string Responsibilities { get; set; }

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

        [Display(Name = "Сортировка по имени")]
        public SortType SortTypeName { get; set; }

        [Display(Name = "Сортировка по фамилии")]
        public SortType SortTypeSurname { get; set; }

        [Display(Name = "Сортировка по отчеству")]
        public SortType SortTypeMiddleName { get; set; }

        [Display(Name = "Сортировка по зарплате")]
        public SortType SortTypeSalary { get; set; }

        [Display(Name = "Сортировка по проценту от сделки")]
        public SortType SortTypeTransactionPercent { get; set; }

        [Display(Name = "Сортировка по продолжительности контракта")]
        public SortType SortTypeContractDuration { get; set; }
    }
}