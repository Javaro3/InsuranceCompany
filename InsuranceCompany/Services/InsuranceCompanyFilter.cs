using InsuranceCompany.Data.Utilities;
using InsuranceCompany.ViewModels;
using InsuranceCompany.Models;
using Microsoft.IdentityModel.Tokens;

namespace InsuranceCompany.Services {
    public class InsuranceCompanyFilter {
        private readonly InsuranceCompanyCache _cache;

        public InsuranceCompanyFilter(InsuranceCompanyCache cache) {
            _cache = cache;
        }

        public IEnumerable<InsuranceType> Filter(InsuranceTypeFilter filter) {
            var result = _cache.GetEntity<InsuranceType>();
            if(filter != null) {
                return result
                    .Where(e => filter.Name.IsNullOrEmpty() || e.Name.StartsWith(filter.Name))
                    .Where(e => filter.Description.IsNullOrEmpty() || e.Description.StartsWith(filter.Description));
            }
            return result;
        }
    }
}
