namespace InsuranceCompany.Models {
    public class PolicyClient {
        public int Id { get; set; }

        public int PolicyId { get; set; }

        public int ClientId { get; set; }

        public virtual Policy Policy { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
    }
}
