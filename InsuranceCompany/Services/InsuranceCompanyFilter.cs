using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.Utilities;
using InsuranceCompany.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace InsuranceCompany.Services {
    public class InsuranceCompanyFilter {
        private readonly InsuranceCompanyCache _cache;

        public InsuranceCompanyFilter(InsuranceCompanyCache cache) {
            _cache = cache;
        }

        public IEnumerable<InsuranceType> Filter(InsuranceTypeFilterModel filter) {
            var result = _cache.GetEntity<InsuranceType>();
            if (filter != null) {
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

        public IEnumerable<Client> Filter(ClientFilterModel filter) {
            var nowTime = DateTime.Now;
            var result = _cache.GetEntity<Client>();
            var policyClients = _cache.GetEntity<PolicyClient>();
            if (filter != null) {
                return result
                    .Where(e => filter.Name == null || filter.Name == e.Name)
                    .Where(e => filter.Surname == null || filter.Surname == e.Surname)
                    .Where(e => filter.MiddleName == null || filter.MiddleName == e.MiddleName)
                    .Where(e => filter.PassportNumber == null || e.PassportNumber.Contains(filter.PassportNumber))
                    .Where(e => filter.Address == null || e.Address.Contains(filter.Address))
                    .Where(e => filter.MobilePhone == null || e.MobilePhone.Contains(filter.MobilePhone))
                    .Where(e => filter.BirthdateStart == null || filter.BirthdateStart <= e.Birthdate)
                    .Where(e => filter.BirthdateEnd == null || filter.BirthdateEnd >= e.Birthdate)
                    .Where(e => filter.PassportIssueDateStart == null || filter.PassportIssueDateStart <= e.PassportIssueDate)
                    .Where(e => filter.PassportIssueDateEnd == null || filter.PassportIssueDateEnd >= e.PassportIssueDate)
                    .Where(e => {
                        if (!filter.PolicyIsFinishNextMounth) return true;
                        var policyClient = policyClients.FirstOrDefault(m => m.ClientId == e.Id);
                        if (policyClient == null) return false;
                        var dayDiff = (policyClient.Policy.ApplicationDate.AddMonths(policyClient.Policy.PolicyTerm) - nowTime).TotalDays;
                        return dayDiff < 30 && dayDiff > 0;
                    })
                    .Sort(filter);
            }
            return result;
        }

        public IEnumerable<InsuranceAgent> Filter(InsuranceAgentFilterModel filter) {
            var result = _cache.GetEntity<InsuranceAgent>();
            if (filter != null) {
                return result
                    .Where(e => filter.Name == null || filter.Name == e.Name)
                    .Where(e => filter.Surname == null || filter.Surname == e.Surname)
                    .Where(e => filter.MiddleName == null || filter.MiddleName == e.MiddleName)
                    .Where(e => filter.Responsibilities == null || filter.Responsibilities == e.Contract.Responsibilities)
                    .Where(e => filter.AgentType == null || filter.AgentType == e.AgentType.Type)
                    .Where(e => filter.StartDeadLine == null || e.Contract.StartDeadline >= filter.StartDeadLine)
                    .Where(e => filter.EndDeadLine == null || e.Contract.EndDeadline <= filter.EndDeadLine)
                    .Where(e => filter.MinSalary == null || e.Contract.Salary >= filter.MinSalary)
                    .Where(e => filter.MaxSalary == null || e.Contract.Salary <= filter.MaxSalary)
                    .Where(e => filter.MinTransactionPercent == null || e.Contract.TransactionPercent >= filter.MinTransactionPercent)
                    .Where(e => filter.MaxTransactionPercent == null || e.Contract.TransactionPercent <= filter.MaxTransactionPercent)
                    .Sort(filter);
            }
            return result;
        }

        public IEnumerable<InsuranceCase> Filter(InsuranceCaseFilterModel filter) {
            var result = _cache.GetEntity<InsuranceCase>();
            if (filter != null) {
                return result
                    .Where(e => filter.SupportingDocument == null || e.SupportingDocument.Name.Contains(filter.SupportingDocument))
                    .Where(e => filter.StartDate == null || filter.StartDate <= e.Date)
                    .Where(e => filter.EndDate == null || filter.EndDate >= e.Date)
                    .Where(e => filter.MinInsurancePayment == null || e.InsurancePayment >= filter.MinInsurancePayment)
                    .Where(e => filter.MaxInsurancePayment == null || e.InsurancePayment <= filter.MaxInsurancePayment)
                    .Sort(filter);
            }
            return result;
        }

        public IEnumerable<SupportingDocument> Filter(SupportingDocumentFilterModel filter) {
            var result = _cache.GetEntity<SupportingDocument>();
            if (filter != null) {
                return result
                    .Where(e => filter.Name == null || e.Name.Contains(filter.Name))
                    .Sort(filter);
            }
            return result;
        }

        public IEnumerable<Policy> Filter(PolicyFilterModel filter, int clientId) {
            var policiesId = Filter(filter).Select(e => e.Id);
            var result = _cache.GetEntity<PolicyClient>();
            if (filter != null) {
                return result
                    .Where(e => e.ClientId == clientId)
                    .Where(e => policiesId.Contains(e.PolicyId))
                    .Select(e => e.Policy)
                    .Sort(filter);
            }
            return result.Select(e => e.Policy);
        }
    }
}
