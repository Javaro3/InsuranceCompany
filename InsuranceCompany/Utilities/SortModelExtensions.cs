using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;
using Microsoft.DiaSymReader;

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

            contracts = filter.SortTypeContractDuration switch {
                SortType.AscendingSort => contracts.OrderBy(e => e.EndDeadline - e.StartDeadline),
                SortType.DescendingSort => contracts.OrderByDescending(e => e.EndDeadline - e.StartDeadline),
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

        public static IEnumerable<Client> Sort(this IEnumerable<Client> clients, ClientFilterModel filter) {
            clients = filter.SortTypeSurname switch {
                SortType.AscendingSort => clients.OrderBy(e => e.Surname),
                SortType.DescendingSort => clients.OrderByDescending(e => e.Surname),
                _ => clients
            };

            clients = filter.SortTypeName switch {
                SortType.AscendingSort => clients.OrderBy(e => e.Name),
                SortType.DescendingSort => clients.OrderByDescending(e => e.Name),
                _ => clients
            };

            clients = filter.SortTypeMiddleName switch {
                SortType.AscendingSort => clients.OrderBy(e => e.MiddleName),
                SortType.DescendingSort => clients.OrderByDescending(e => e.MiddleName),
                _ => clients
            };

            clients = filter.SortTypeBirthdate switch {
                SortType.AscendingSort => clients.OrderBy(e => e.Birthdate),
                SortType.DescendingSort => clients.OrderByDescending(e => e.Birthdate),
                _ => clients
            };

            clients = filter.SortTypePassportIssueDate switch {
                SortType.AscendingSort => clients.OrderBy(e => e.PassportIssueDate),
                SortType.DescendingSort => clients.OrderByDescending(e => e.PassportIssueDate),
                _ => clients
            };

            return clients;
        }

        public static IEnumerable<InsuranceAgent> Sort(this IEnumerable<InsuranceAgent> insuranceAgents, InsuranceAgentFilterModel filter) {
            insuranceAgents = filter.SortTypeSurname switch {
                SortType.AscendingSort => insuranceAgents.OrderBy(e => e.Surname),
                SortType.DescendingSort => insuranceAgents.OrderByDescending(e => e.Surname),
                _ => insuranceAgents
            };

            insuranceAgents = filter.SortTypeName switch {
                SortType.AscendingSort => insuranceAgents.OrderBy(e => e.Name),
                SortType.DescendingSort => insuranceAgents.OrderByDescending(e => e.Name),
                _ => insuranceAgents
            };

            insuranceAgents = filter.SortTypeMiddleName switch {
                SortType.AscendingSort => insuranceAgents.OrderBy(e => e.MiddleName),
                SortType.DescendingSort => insuranceAgents.OrderByDescending(e => e.MiddleName),
                _ => insuranceAgents
            };

            insuranceAgents = filter.SortTypeSalary switch {
                SortType.AscendingSort => insuranceAgents.OrderBy(e => e.Contract.Salary),
                SortType.DescendingSort => insuranceAgents.OrderByDescending(e => e.Contract.Salary),
                _ => insuranceAgents
            };

            insuranceAgents = filter.SortTypeTransactionPercent switch {
                SortType.AscendingSort => insuranceAgents.OrderBy(e => e.Contract.TransactionPercent),
                SortType.DescendingSort => insuranceAgents.OrderByDescending(e => e.Contract.TransactionPercent),
                _ => insuranceAgents
            };

            insuranceAgents = filter.SortTypeContractDuration switch {
                SortType.AscendingSort => insuranceAgents.OrderBy(e => e.Contract.EndDeadline - e.Contract.StartDeadline),
                SortType.DescendingSort => insuranceAgents.OrderByDescending(e => e.Contract.EndDeadline - e.Contract.StartDeadline),
                _ => insuranceAgents
            };

            return insuranceAgents;
        }

        public static IEnumerable<InsuranceCase> Sort(this IEnumerable<InsuranceCase> insuranceCase, InsuranceCaseFilterModel filter) {
            insuranceCase = filter.SortTypeDate switch {
                SortType.AscendingSort => insuranceCase.OrderBy(e => e.Date),
                SortType.DescendingSort => insuranceCase.OrderByDescending(e => e.Date),
                _ => insuranceCase
            };

            insuranceCase = filter.SortTypeInsurancePayment switch {
                SortType.AscendingSort => insuranceCase.OrderBy(e => e.InsurancePayment),
                SortType.DescendingSort => insuranceCase.OrderByDescending(e => e.InsurancePayment),
                _ => insuranceCase
            };

            insuranceCase = filter.SortTypeSupportingDocument switch {
                SortType.AscendingSort => insuranceCase.OrderBy(e => e.SupportingDocument.Name),
                SortType.DescendingSort => insuranceCase.OrderByDescending(e => e.SupportingDocument.Name),
                _ => insuranceCase
            };

            return insuranceCase;
        }

        public static IEnumerable<SupportingDocument> Sort(this IEnumerable<SupportingDocument> supportingDocuments, SupportingDocumentFilterModel filter) {
            supportingDocuments = filter.SortTypeName switch {
                SortType.AscendingSort => supportingDocuments.OrderBy(e => e.Name),
                SortType.DescendingSort => supportingDocuments.OrderByDescending(e => e.Name),
                _ => supportingDocuments
            };

            return supportingDocuments;
        }
    }
}
