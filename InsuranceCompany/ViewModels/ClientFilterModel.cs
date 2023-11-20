using InsuranceCompany.Data.Utilities;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.ViewModels {
    public class ClientFilterModel {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthdateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthdateEnd { get; set; }
        
        public string Address { get; set; }
        public string MobilePhone { get; set; }

        [MaxLength(9)]
        public string PassportNumber { get; set; }


        [DataType(DataType.Date)]
        public DateTime? PassportIssueDateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PassportIssueDateEnd { get; set; }

        public bool PolicyIsFinishNextMounth { get; set; }

        public SortType SortTypeName { get; set; }
        public SortType SortTypeSurname { get; set; }
        public SortType SortTypeMiddleName { get; set; }
        public SortType SortTypeBirthdate { get; set; }
        public SortType SortTypePassportIssueDate { get; set; }
    }
}
