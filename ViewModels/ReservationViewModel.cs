using System.ComponentModel.DataAnnotations;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.ViewModels {
    public class ReservationViewModel {
        [Required]
        public string ID { get; set; }

        [Display(Name = "Pojazd")]
        [Required(ErrorMessage = "Pole Pojazd jest wymagane.")]
        public VehicleItemViewModel Vehicle { get; set; }

        [Display(Name = "Użytkownik")]
        [Required(ErrorMessage = "Pole Użytkownik jest wymagane.")]
        public UserViewModel User { get; set; }

        [Display(Name = "Punkt Wypożyczenia")]
        [Required(ErrorMessage = "Pole Punkt Wypożyczenia jest wymagane.")]
        public ReservationPoint ReservationPoint { get; set; }

        [Display(Name = "Punkt Zwrotu")]
        public ReservationPoint? ReturnPoint { get; set; }

        [Display(Name = "Data Wypożyczenia")]
        [Range(typeof(DateTime), "1/1/2000", "31/12/2100", ErrorMessage = "Data Wypożyczenia musi być pomiędzy 1.01.2000 a 31.12.2100")]
        [Required(ErrorMessage = "Pole Data Wypożyczenia jest wymagane.")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Data Zwrotu")]
        [Range(typeof(DateTime), "1/1/2000", "31/12/2100", ErrorMessage = "Data Zwrotu musi być pomiędzy 1.01.2000 a 31.12.2100")]
        public DateTime? ReturnDate { get; set; }
    }
}