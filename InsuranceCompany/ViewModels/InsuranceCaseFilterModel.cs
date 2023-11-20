using InsuranceCompany.Data.Utilities;

namespace InsuranceCompany.ViewModels {
    public class InsuranceCaseFilterModel {
        public string SupportingDocument { get; set; }
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? MinInsurancePayment { get; set; }
        public decimal? MaxInsurancePayment { get; set; }

        public SortType SortTypeInsurancePayment { get; set; }
        public SortType SortTypeDate { get; set; }
        public SortType SortTypeSupportingDocument { get; set; }
    }
}
