using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Models {
    public class Reservation {
        [Key]
        public string ID { get; set; }

        [Required(ErrorMessage = "Pojazd jest wymagany.")]
        [ForeignKey("Vehicle")]
        public Vehicle VehicleID { get; set; }

        [Required(ErrorMessage = "Status zamówienia jest wymagany.")]
        [Display(Name = "Status zamówienia")]
        public ReservationStatusEnum ReservationStatus { get; set; }

        [Required(ErrorMessage = "Użytkownik jest wymagany.")]
        [ForeignKey("User")]
        public User UserID { get; set; }

        [ForeignKey("ReservationPoint")]
        public int ReservationPointID { get; set; }
        public virtual ReservationPoint ReservationPoint { get; set; }
        
        [ForeignKey("ReturnPoint")]
        public int ReturnPointID { get; set; }
        public virtual ReservationPoint? ReturnPoint { get; set; }

        [Required(ErrorMessage = "Data rezerwacji jest wymagana.")]
        [Display(Name = "Data rezerwacji")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Data zwrotu")]
        public DateTime? ReturnDate { get; set; }
    }
}