using InsuranceCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Data;

public partial class InsuranceCompanyContext : DbContext {
    public InsuranceCompanyContext() { }

    public InsuranceCompanyContext(DbContextOptions<InsuranceCompanyContext> options) : base(options) { }

    public virtual DbSet<AgentType> AgentTypes { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Contract> Contracts { get; set; }
    public virtual DbSet<InsuranceAgent> InsuranceAgents { get; set; }
    public virtual DbSet<InsuranceCase> InsuranceCases { get; set; }
    public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }
    public virtual DbSet<Policy> Policies { get; set; }
    public virtual DbSet<SupportingDocument> SupportingDocuments { get; set; }
    public virtual DbSet<PolicyClient> PolicyClients { get; set; }
}
