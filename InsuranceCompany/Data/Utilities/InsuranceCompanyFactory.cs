namespace InsuranceCompany.Data.Utilities {
    public static class InsuranceCompanyFactory {
        public static IEnumerable<T> GetEnites<T>(string entityName, InsuranceCompanyContext db) {
            switch (entityName) {
                case "AgentType":
                    return (IEnumerable<T>)db.AgentTypes.ToList();
                case "Client":
                    return (IEnumerable<T>)db.Clients.ToList();
                case "Contract":
                    return (IEnumerable<T>)db.Contracts.ToList();
                case "InsuranceAgent":
                    return (IEnumerable<T>)db.InsuranceAgents.ToList();
                case "InsuranceCase":
                    return (IEnumerable<T>)db.InsuranceCases.ToList();
                case "InsuranceType":
                    return (IEnumerable<T>)db.InsuranceTypes.ToList();
                case "Policy":
                    return (IEnumerable<T>)db.Policies.ToList();
                case "PolicyInsuranceCase":
                    return (IEnumerable<T>)db.PolicyInsuranceCases.ToList();
                case "SupportingDocument":
                    return (IEnumerable<T>)db.SupportingDocuments.ToList();
            }
            return null;
        }
    }
}