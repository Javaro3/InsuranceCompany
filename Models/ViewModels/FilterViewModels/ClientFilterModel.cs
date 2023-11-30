using Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.FilterViewModels {
    public class ClientFilterModel {
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения ОТ")]
        public DateTime? BirthdateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения ДО")]
        public DateTime? BirthdateEnd { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Номер телефона")]
        public string MobilePhone { get; set; }

        [MaxLength(9)]
        [Display(Name = "Номер пасспорта")]
        public string PassportNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата выдачи пасспорта ОТ")]
        public DateTime? PassportIssueDateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата выдачи пасспорта ДО")]
        public DateTime? PassportIssueDateEnd { get; set; }

        [Display(Name = "Полис истекает в следующем месяце")]
        public bool PolicyIsFinishNextMounth { get; set; }

        [Display(Name = "Сортировка по имени")]
        public SortType SortTypeName { get; set; }

        [Display(Name = "Сортировка по фамилии")]
        public SortType SortTypeSurname { get; set; }

        [Display(Name = "Сортировка по отчеству")]
        public SortType SortTypeMiddleName { get; set; }

        [Display(Name = "Сортировка по дате рождения")]
        public SortType SortTypeBirthdate { get; set; }

        [Display(Name = "Сортировка по дате выдачи паспорта")]
        public SortType SortTypePassportIssueDate { get; set; }
    }
}