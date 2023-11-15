using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;

namespace InsuranceCompany.Utilities {
    public static class SortModelExtensions {
        public static IEnumerable<InsuranceType> Sort(this IEnumerable<InsuranceType> insuranceTypes, InsuranceTypeFilterModel filter) {
            insuranceTypes = filter.SortTypeName switch {
                SortType.AscendingSort => insuranceTypes.OrderBy(e => e.Name),
                SortType.DescendingSort => insuranceTypes.OrderByDescending(e => e.Name),
                _ => insuranceTypes
            };
            insuranceTypes = filter.SortTypeDescription switch {
                SortType.AscendingSort => insuranceTypes.OrderBy(e => e.Description),
                SortType.DescendingSort => insuranceTypes.OrderByDescending(e => e.Description),
                _ => insuranceTypes
            };
            return insuranceTypes;
        }

        public static IEnumerable<Contract> Sort(this IEnumerable<Contract> contracts, ContractFilterModel filter) {
            contracts = filter.SortTypeResponsobilities switch {
                SortType.AscendingSort => contracts.OrderBy(e => e.Responsibilities),
                SortType.DescendingSort => contracts.OrderByDescending(e => e.Responsibilities),
                _ => contracts
            };
            contracts = filter.SortTypeTransactionPercent switch {
                SortType.AscendingSort => contracts.OrderBy(e => e.TransactionPercent),
                SortType.DescendingSort => contracts.OrderByDescending(e => e.TransactionPercent),
                _ => contracts
            };
            contracts = filter.SortTypeSalary switch {
                SortType.AscendingSort => contracts.OrderBy(e => e.Salary),
                SortType.DescendingSort => contracts.OrderByDescending(e => e.Salary),
                _ => contracts
            };

            return contracts;
        }

        public static IEnumerable<Policy> Sort(this IEnumerable<Policy> policies, PolicyFilterModel filter) {
            policies = filter.SortTypeApplicationDate switch {
                SortType.AscendingSort => policies.OrderBy(e => e.ApplicationDate),
                SortType.DescendingSort => policies.OrderByDescending(e => e.ApplicationDate),
                _ => policies
            };
            policies = filter.SortTypePolicyTerm switch {
                SortType.AscendingSort => policies.OrderBy(e => e.PolicyTerm),
                SortType.DescendingSort => policies.OrderByDescending(e => e.PolicyTerm),
                _ => policies
            };
            policies = filter.SortTypePolicyPaymen switch {
                SortType.AscendingSort => policies.OrderBy(e => e.PolicyPayment),
                SortType.DescendingSort => policies.OrderByDescending(e => e.PolicyPayment),
                _ => policies
            };

            return policies;
        }
    }
}
