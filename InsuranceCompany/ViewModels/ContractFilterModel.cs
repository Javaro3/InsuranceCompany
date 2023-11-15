using InsuranceCompany.Data.Utilities;

namespace InsuranceCompany.ViewModels {
    public class ContractFilterModel {
        public string Responsibilities { get; set; } = null!;
        
        public DateTime? StartDeadLine { get; set; }
        public DateTime? EndDeadLine { get; set; }

        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        public double? MinTransactionPercent { get; set; }
        public double? MaxTransactionPercent { get; set; }

        public SortType SortTypeResponsobilities { get; set; }
        public SortType SortTypeSalary { get; set; }
        public SortType SortTypeTransactionPercent { get; set; }
    }
}
