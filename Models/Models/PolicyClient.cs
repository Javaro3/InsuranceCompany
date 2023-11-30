using System.ComponentModel.DataAnnotations;

namespace Models.Models {
    public class PolicyClient {
        public int Id { get; set; }

        [Display(Name = "Полис")]
        [Required(ErrorMessage = "Полис это обязательное поле")]
        public int PolicyId { get; set; }

        [Display(Name = "Клиент")]
        [Required(ErrorMessage = "Клиент это обязательное поле")]
        public int ClientId { get; set; }

        public virtual Policy Policy { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
    }
}
