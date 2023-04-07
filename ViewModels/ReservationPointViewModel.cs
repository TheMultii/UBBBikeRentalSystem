using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public class ReservationPointViewModel {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(50, ErrorMessage = "Nazwa może mieć maksymalnie 50 znaków")]
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Adres jest wymagany")]
        [StringLength(100, ErrorMessage = "Adres może mieć maksymalnie 100 znaków")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Miasto jest wymagane")]
        [StringLength(50, ErrorMessage = "Miasto może mieć maksymalnie 50 znaków")]
        [Display(Name = "Miasto")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy musi być w formacie XX-XXX")]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
        
        [Required(ErrorMessage = "Szerokość geograficzna jest wymagana")]
        [Range(-90, 90, ErrorMessage = "Szerokość geograficzna musi być między -90 a 90")]
        [Display(Name = "Szer. geogr.")]
        public double Latitude { get; set; }
        
        [Required(ErrorMessage = "Długość geograficzna jest wymagana")]
        [Range(-180, 180, ErrorMessage = "Długość geograficzna musi być między -180 a 180")]
        [Display(Name = "Dł. geogr.")]
        public double Longitude { get; set; }
    }
}
