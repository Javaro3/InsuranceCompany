using Microsoft.EntityFrameworkCore;
using Repository;

namespace Service {
    public static class InsuranceCompanyFactory {
        public static IEnumerable<T> GetEnites<T>(string entityName, InsuranceCompanyContext db) {
            return entityName switch {
                "AgentType" => GetAgentTypes<T>(db),
                "Client" => GetClients<T>(db),
                "Contract" => GetContracts<T>(db),
                "InsuranceType" => GetInsuranceTypes<T>(db),
                "SupportingDocument" => GetSupportingDocuments<T>(db),
                "InsuranceAgent" => GetInsuranceAgents<T>(db),
                "InsuranceCase" => GetInsuranceCases<T>(db),
                "Policy" => GetPolicies<T>(db),
                "PolicyClient" => GetPolicyClients<T>(db),
                _ => throw new NullReferenceException(),
            };
        }

        private static IEnumerable<T> GetAgentTypes<T>(InsuranceCompanyContext db) => (IEnumerable<T>)db.AgentTypes.ToList();

        private static IEnumerable<T> GetClients<T>(InsuranceCompanyContext db) => (IEnumerable<T>)db.Clients.ToList();

        private static IEnumerable<T> GetContracts<T>(InsuranceCompanyContext db) => (IEnumerable<T>)db.Contracts.ToList();

        private static IEnumerable<T> GetInsuranceTypes<T>(InsuranceCompanyContext db) => (IEnumerable<T>)db.InsuranceTypes.ToList();

        private static IEnumerable<T> GetSupportingDocuments<T>(InsuranceCompanyContext db) => (IEnumerable<T>)db.SupportingDocuments.ToList();

        private static IEnumerable<T> GetInsuranceAgents<T>(InsuranceCompanyContext db) {
            return (IEnumerable<T>)db.InsuranceAgents
                .Include(e => e.AgentType)
                .Include(e => e.Contract)
                .ToList();
        }

        private static IEnumerable<T> GetInsuranceCases<T>(InsuranceCompanyContext db) {
            return (IEnumerable<T>)db.InsuranceCases
                .Include(e => e.Client)
                .Include(e => e.SupportingDocument)
                .ToList();
        }

        private static IEnumerable<T> GetPolicies<T>(InsuranceCompanyContext db) {
            return (IEnumerable<T>)db.Policies
                .Include(e => e.InsuranceType)
                .Include(e => e.InsuranceAgent)
                    .ThenInclude(e => e.Contract)
                .Include(e => e.InsuranceAgent)
                    .ThenInclude(e => e.AgentType)
                .ToList();
        }

        private static IEnumerable<T> GetPolicyClients<T>(InsuranceCompanyContext db) {
            return (IEnumerable<T>)db.PolicyClients
                .Include(e => e.Client)
                .Include(e => e.Policy)
                    .ThenInclude(e => e.InsuranceType)
                .Include(e => e.Policy)
                    .ThenInclude(e => e.InsuranceAgent)
                        .ThenInclude(e => e.AgentType)
                .Include(e => e.Policy)
                    .ThenInclude(e => e.InsuranceAgent)
                        .ThenInclude(e => e.Contract)
                .ToList();
        }
    }
}