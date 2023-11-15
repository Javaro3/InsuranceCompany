using InsuranceCompany.Data.Utilities;
using InsuranceCompany.ViewModels;
using InsuranceCompany.Models;
using Microsoft.IdentityModel.Tokens;
using InsuranceCompany.Utilities;

namespace InsuranceCompany.Services {
    public class InsuranceCompanyFilter {
        private readonly InsuranceCompanyCache _cache;

        public InsuranceCompanyFilter(InsuranceCompanyCache cache) {
            _cache = cache;
        }

        public IEnumerable<InsuranceType> Filter(InsuranceTypeFilterModel filter) {
            var result = _cache.GetEntity<InsuranceType>();
            if(filter != null) {
                return result
                    .Where(e => filter.Name.IsNullOrEmpty() || e.Name.StartsWith(filter.Name))
                    .Where(e => filter.Description.IsNullOrEmpty() || e.Description.StartsWith(filter.Description))
                    .Sort(filter);
            }
            return result;
        }

        public IEnumerable<Contract> Filter(ContractFilterModel filter) {
            var result = _cache.GetEntity<Contract>();
            if (filter != null) {
                return result
                    .Where(e => filter.Responsibilities.IsNullOrEmpty() || e.Responsibilities.StartsWith(filter.Responsibilities))
                    .Where(e => filter.StartDeadLine == null || e.StartDeadline >= filter.StartDeadLine)
                    .Where(e => filter.EndDeadLine == null || e.EndDeadline <= filter.EndDeadLine)
                    .Where(e => filter.MinSalary == null || e.Salary >= filter.MinSalary)
                    .Where(e => filter.MaxSalary == null || e.Salary <= filter.MaxSalary)
                    .Where(e => filter.MinTransactionPercent == null || e.TransactionPercent >= filter.MinTransactionPercent)
                    .Where(e => filter.MaxTransactionPercent == null || e.TransactionPercent <= filter.MaxTransactionPercent)
                    .Sort(filter);
            }
            return result;
        }

        public IEnumerable<Policy> Filter(PolicyFilterModel filter) {
            var nowTime = DateTime.Now;
            var result = _cache.GetEntity<Policy>();
            if (filter != null) {
                return result
                    .Where(e => filter.InsuranceTypeId == null || filter.InsuranceTypeId == -1 || filter.InsuranceTypeId == e.InsuranceTypeId)
                    .Where(e => filter.InsuranceAgentId == null || filter.InsuranceAgentId == -1 || filter.InsuranceAgentId == e.InsuranceAgentId)
                    .Where(e => filter.PolicyNumber == null || filter.PolicyNumber == e.PolicyNumber)
                    .Where(e => filter.ApplicationDateStart == null || filter.ApplicationDateStart <= e.ApplicationDate)
                    .Where(e => filter.ApplicationDateEnd == null || filter.ApplicationDateEnd >= e.ApplicationDate.AddMonths(e.PolicyTerm))
                    .Where(e => filter.MinPolicyPayment == null || e.PolicyPayment >= filter.MinPolicyPayment)
                    .Where(e => filter.MaxPolicyPayment == null || e.PolicyPayment <= filter.MaxPolicyPayment)
                    .Where(e => !filter.PolicyIsActing || (filter.PolicyIsActing && nowTime >= e.ApplicationDate && nowTime <= e.ApplicationDate.AddMonths(e.PolicyTerm)))
                    .Sort(filter);
            }
            return result;
        }
    }
}
