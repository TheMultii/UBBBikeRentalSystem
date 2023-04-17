using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public class UserViewModel {
        [Required]
        public string ID { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }
    }
}
