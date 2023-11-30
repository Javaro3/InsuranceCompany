using Models.Models;
using Models.Utilities;
using Models.ViewModels.FilterViewModels;
using System.Linq.Expressions;

namespace Service {
    public static class SortExtensions {
        public static IEnumerable<InsuranceType> Sort(this IEnumerable<InsuranceType> insuranceTypes, InsuranceTypeFilterModel filter) {
            return insuranceTypes
                .Sort(filter.SortTypeName, e => e.Name)
                .Sort(filter.SortTypeDescription, e => e.Description);
        }

        public static IEnumerable<Contract> Sort(this IEnumerable<Contract> contracts, ContractFilterModel filter) {
            return contracts
                .Sort(filter.SortTypeResponsobilities, e => e.Responsibilities)
                .Sort(filter.SortTypeTransactionPercent, e => e.TransactionPercent)
                .Sort(filter.SortTypeSalary, e => e.Salary)
                .Sort(filter.SortTypeContractDuration, e => e.EndDeadline - e.StartDeadline);
        }

        public static IEnumerable<Policy> Sort(this IEnumerable<Policy> policies, PolicyFilterModel filter) {
            return policies
                .Sort(filter.SortTypeApplicationDate, e => e.ApplicationDate)
                .Sort(filter.SortTypePolicyTerm, e => e.PolicyTerm)
                .Sort(filter.SortTypePolicyPaymen, e => e.PolicyPayment);
        }

        public static IEnumerable<Client> Sort(this IEnumerable<Client> clients, ClientFilterModel filter) {
            return clients
               .Sort(filter.SortTypeSurname, e => e.Surname)
               .Sort(filter.SortTypeName, e => e.Name)
               .Sort(filter.SortTypeMiddleName, e => e.MiddleName)
               .Sort(filter.SortTypeBirthdate, e => e.Birthdate)
               .Sort(filter.SortTypePassportIssueDate, e => e.PassportIssueDate);
        }

        public static IEnumerable<InsuranceAgent> Sort(this IEnumerable<InsuranceAgent> insuranceAgents, InsuranceAgentFilterModel filter) {
            return insuranceAgents
                .Sort(filter.SortTypeSurname, e => e.Surname)
                .Sort(filter.SortTypeName, e => e.Name)
                .Sort(filter.SortTypeMiddleName, e => e.MiddleName)
                .Sort(filter.SortTypeSalary, e => e.Contract.Salary)
                .Sort(filter.SortTypeTransactionPercent, e => e.Contract.TransactionPercent)
                .Sort(filter.SortTypeContractDuration, e => e.Contract.EndDeadline - e.Contract.StartDeadline);
        }

        public static IEnumerable<InsuranceCase> Sort(this IEnumerable<InsuranceCase> insuranceCase, InsuranceCaseFilterModel filter) {
            return insuranceCase
                .Sort(filter.SortTypeDate, e => e.Date)
                .Sort(filter.SortTypeInsurancePayment, e => e.InsurancePayment)
                .Sort(filter.SortTypeSupportingDocument, e => e.SupportingDocument.Name);
        }

        public static IEnumerable<SupportingDocument> Sort(this IEnumerable<SupportingDocument> supportingDocuments, SupportingDocumentFilterModel filter) {
            return supportingDocuments
                .Sort(filter.SortTypeName, e => e.Name);
        }

        private static IEnumerable<T> Sort<T, K>(this IEnumerable<T> entities, SortType sortType, Expression<Func<T, K>> sortExpression) {
            entities = sortType switch {
                SortType.AscendingSort => entities.AsQueryable().OrderBy(sortExpression),
                SortType.DescendingSort => entities.AsQueryable().OrderByDescending(sortExpression),
                _ => entities
            };
            return entities;
        }
    }
}