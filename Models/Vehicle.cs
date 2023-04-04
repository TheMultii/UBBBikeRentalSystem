using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.Models {
    public class Vehicle {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Model jest wymagany.")]
        public string? Model { get; set; }

        [Url(ErrorMessage = "Adres URL musi być prawidłowy.")]
        public string? ImageUrl { get; set; }

        public virtual VehicleType? VehicleType { get; set; }

        [Required(ErrorMessage = "Cena za godzinę jest wymagana.")]
        [Range(1, int.MaxValue, ErrorMessage = "Cena za godzinę musi być większa od 0.")]
        public int PricePerHour { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Status dostępności jest wymagany.")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Data produkcji jest wymagana.")]
        [Display(Name = "Data produkcji")]
        public DateTime ManufactureDate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Maksymalna prędkość musi być większa od 0.")]
        public double? MaxSpeed { get; set; }
    }
}