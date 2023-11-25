﻿using InsuranceCompany.Data.Utilities;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.ViewModels {
    public class InsuranceTypeFilterModel {
        [Display(Name = "Название")]
        public string Name { get; set; } = null!;

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Сортировка по имени")]
        public SortType SortTypeName { get; set; }

        [Display(Name = "Сортировка по описанию")]
        public SortType SortTypeDescription { get; set; }
    }
}
