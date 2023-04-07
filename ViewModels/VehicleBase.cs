using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public abstract class VehicleBase {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "Pole Nazwa jest wymagane.")]
        [StringLength(50, ErrorMessage = "Pole Nazwa musi mieć długość od {2} do {1} znaków.", MinimumLength = 3)]
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Pole Model jest wymagane.")]
        [StringLength(50, ErrorMessage = "Pole Model musi mieć długość od {2} do {1} znaków.", MinimumLength = 3)]
        public string? Model { get; set; }

        [Display(Name = "URL Grafiki")]
        [Url(ErrorMessage = "Pole URL Grafiki musi zawierać prawidłowy adres URL.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Pole Typ pojazdu jest wymagane.")]
        [Display(Name = "Typ pojazdu")]
        public VehicleTypeEnum? VehicleType { get; set; }

        [Required(ErrorMessage = "Pole Cena za godzinę jest wymagane.")]
        [Range(1, 100, ErrorMessage = "Pole Cena za godzinę musi zawierać wartość z przedziału {1}-{2}.")]
        [Display(Name = "Cena za godzinę")]
        [DisplayFormat(DataFormatString = "{0} zł/h")]
        public int PricePerHour { get; set; }
    }
}
