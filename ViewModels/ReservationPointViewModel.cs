using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public class ReservationPointViewModel {
        public int ID { get; set; }
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }
        [Display(Name = "Adres")]
        public string? Address { get; set; }
        [Display(Name = "Miasto")]
        public string? City { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string? PostalCode { get; set; }
        [Display(Name = "Szer. geogr.")]
        public double? Latitude { get; set; }
        [Display(Name = "Dł. geogr.")]
        public double? Longitude { get; set; }
    }
}
