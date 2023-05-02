using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public class UserViewModel {
        [Required]
        public string ID { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)] 
        public string Name { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }
    }
}
