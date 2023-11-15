using InsuranceCompany.Data.Utilities;

namespace InsuranceCompany.ViewModels {
    public class InsuranceTypeFilterModel {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public SortType SortTypeName { get; set; }

        public SortType SortTypeDescription { get; set; }
    }
}
