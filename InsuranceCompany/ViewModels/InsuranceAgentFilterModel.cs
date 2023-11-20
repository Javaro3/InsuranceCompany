using InsuranceCompany.Data.Utilities;

namespace InsuranceCompany.ViewModels {
    public class InsuranceAgentFilterModel {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string AgentType { get; set; }
        public string Responsibilities { get; set; }

        public DateTime? StartDeadLine { get; set; }
        public DateTime? EndDeadLine { get; set; }

        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        public double? MinTransactionPercent { get; set; }
        public double? MaxTransactionPercent { get; set; }


        public SortType SortTypeName { get; set; }
        public SortType SortTypeSurname { get; set; }
        public SortType SortTypeMiddleName { get; set; }
        public SortType SortTypeSalary { get; set; }
        public SortType SortTypeTransactionPercent { get; set; }
        public SortType SortTypeContractDuration { get; set; }
    }
}
