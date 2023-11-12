using InsuranceCompany.Data.Utilities;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Data.DbInitializer {
    public static class InsuranceCompanyInitializer {
        private static Random rand = new();

        public static void Initialize(InsuranceCompanyContext db, InsuranceCompanyIdentityContext identityDb, UserManager<ApplicationUser> userManager) {
            if (!db.Database.CanConnect()) {
                db.Database.Migrate();
            }

            if (!identityDb.Roles.Any()) {
                InitializeIdentityRoles(identityDb);
            }

            if (!db.AgentTypes.Any()) {
                InitializeAgentTypes(db);
            }

            if (!db.Contracts.Any()) {
                InitializeContracts(db);
            }

            if (!db.InsuranceAgents.Any()) {
                InitializeInsuranceAgents(db, userManager);
            }

            if (!db.SupportingDocuments.Any()) {
                InitializeSupportingDocuments(db);
            }

            if (!db.Clients.Any()) {
                InitializeClients(db, userManager);
            }

            if (!db.InsuranceCases.Any()) {
                InitializeInsuranceCases(db);
            }

            if (!db.InsuranceTypes.Any()) {
                InitializeInsuranceTypes(db);
            }

            if (!db.Policies.Any()) {
                InitializePolicies(db);
            }

            if (!db.PolicyClients.Any()) {
                InitializePolicyClients(db);
            }

            db.SaveChanges();
        }

        private static void InitializeIdentityRoles(InsuranceCompanyIdentityContext identityDb) {
            foreach (var name in InitializeData.IdentityRoleNames) {
                identityDb.Add(new IdentityRole() { Name = name, NormalizedName = name.ToUpper() });
            }
            identityDb.SaveChanges();
        }

        private static void InitializePolicyClients(InsuranceCompanyContext db) {
            for (int i = 0; i < 100; i++) {
                int policyId = rand.Next(1, 101);
                int clientId = rand.Next(1, 51);

                db.Add(new PolicyClient() {
                    PolicyId = policyId,
                    ClientId = clientId
                });
            }
            db.SaveChanges();
        }

        private static void InitializePolicies(InsuranceCompanyContext db) {
            for (int i = 0; i < 100; i++) {
                int insuranceAgentId = rand.Next(1, 51);
                var applicationDate = rand.NextDate(2010, 2023);
                string policyNumber = rand.NextPolicyNumber();
                int insuranceTypeId = rand.Next(1, InitializeData.InsuranceTypeNames.Count + 1);
                int policyTerm = rand.Next(1, 20);
                decimal policyPayment = (decimal)(100 * rand.NextDouble());

                db.Add(new Policy() {
                    InsuranceAgentId = insuranceAgentId,
                    ApplicationDate = applicationDate,
                    PolicyNumber = policyNumber,
                    InsuranceTypeId = insuranceTypeId,
                    PolicyTerm = policyTerm,
                    PolicyPayment = policyPayment
                });
            }
            db.SaveChanges();
        }

        private static void InitializeInsuranceTypes(InsuranceCompanyContext db) {
            foreach (var name in InitializeData.InsuranceTypeNames) {
                db.Add(new InsuranceType() { Name = name, Description = $"Описание - {name}" });
            }
        }

        private static void InitializeClients(InsuranceCompanyContext db, UserManager<ApplicationUser> userManager) {
            for (int i = 0; i < 50; i++) {
                string name = rand.NextItem(InitializeData.Names);
                string surname = rand.NextItem(InitializeData.Surnames);
                string middleName = rand.NextItem(InitializeData.MiddleNames);
                var birthdate = rand.NextDate(1950, 2005);
                string mobilePhone = rand.NextPhoneNumber();
                string address = rand.NextItem(InitializeData.Addresses);
                string passportNumber = rand.NextPassportNumber();
                var passportIssueDate = rand.NextDate(2000, 2023);
                string password = $"Client{i}!";
                var userName = DbConverter.GetUserNameTranslator($"{name}_{surname}_{middleName}_Client");

                var applicationUser = new ApplicationUser() {
                    UserName = userName,
                    NormalizedEmail = userName,
                    Name = name,
                    Surname = surname,
                    MiddleName = middleName
                };

                var result = userManager.CreateAsync(applicationUser, password).GetAwaiter().GetResult();
                if (result.Succeeded) {
                    userManager.AddToRoleAsync(applicationUser, "Страховой агент").GetAwaiter().GetResult();
                    db.Add(new Client() {
                        Name = name,
                        Surname = surname,
                        MiddleName = middleName,
                        Birthdate = birthdate,
                        MobilePhone = mobilePhone,
                        Address = address,
                        PassportNumber = passportNumber,
                        PassportIssueDate = passportIssueDate
                    });
                }
                else i--;
            }
            db.SaveChanges();
        }

        private static void InitializeInsuranceCases(InsuranceCompanyContext db) {
            for (int i = 0; i <= 100; i++) {
                var date = rand.NextDate(2010, 2023);
                int supportingDocumentId = rand.Next(1, InitializeData.SupportingDocumentNames.Count + 1);
                decimal insurancePayment = (decimal)(100 * rand.NextDouble());
                int clientId = rand.Next(1, 51);

                db.Add(new InsuranceCase() {
                    Date = date,
                    SupportingDocumentId = supportingDocumentId,
                    InsurancePayment = insurancePayment,
                    ClientId = clientId
                });
            }
            db.SaveChanges();
        }

        private static void InitializeSupportingDocuments(InsuranceCompanyContext db) {
            foreach (var name in InitializeData.SupportingDocumentNames) {
                db.Add(new SupportingDocument() { Name = name, Description = $"Описание - {name}" });
            }
            db.SaveChanges();
        }

        private static void InitializeAgentTypes(InsuranceCompanyContext db) {
            foreach (var type in InitializeData.AgentTypes) {
                db.Add(new AgentType() { Type = type });
            }
            db.SaveChanges();
        }

        private static void InitializeContracts(InsuranceCompanyContext db) {
            for (int i = 0; i < 100; i++) {
                var startDeadline = rand.NextDate(2010, 2023);
                var endDeadline = startDeadline.AddMonths(rand.Next(1, 20));
                var responsibilities = rand.NextItem(InitializeData.Responsibilities);
                decimal salary = (decimal)(100 * rand.NextDouble());
                double transactionPercent = rand.NextDouble();

                db.Add(new Contract() {
                        Responsibilities = responsibilities,
                        StartDeadline = startDeadline,
                        EndDeadline = endDeadline,
                        Salary = salary,
                        TransactionPercent = transactionPercent
                    });
            }
            db.SaveChanges();
        }

        private static void InitializeInsuranceAgents(InsuranceCompanyContext db, UserManager<ApplicationUser> userManager) {
            for (int i = 0; i < 50; i++) {
                string name = rand.NextItem(InitializeData.Names);
                string surname = rand.NextItem(InitializeData.Surnames);
                string middleName = rand.NextItem(InitializeData.MiddleNames);
                int agentTypeId = rand.Next(1, InitializeData.AgentTypes.Count + 1);
                int contractId = rand.Next(1, 101);
                string password = $"InsuranceAgent{i}!";
                var userName = DbConverter.GetUserNameTranslator($"{name}_{surname}_{middleName}_InsuranceAgent");

                var applicationUser = new ApplicationUser() {
                    UserName = userName,
                    NormalizedEmail = userName,
                    Name = name,
                    Surname = surname,
                    MiddleName = middleName
                };

                var result = userManager.CreateAsync(applicationUser, password).GetAwaiter().GetResult();
                if (result.Succeeded) {
                    userManager.AddToRoleAsync(applicationUser, "Страховой агент").GetAwaiter().GetResult();
                    db.Add(new InsuranceAgent() {
                        Name = name,
                        Surname = surname,
                        MiddleName = middleName,
                        AgentTypeId = agentTypeId,
                        ContractId = contractId,
                    });
                }
                else i--;
            }
            db.SaveChanges();
        }
    }
}