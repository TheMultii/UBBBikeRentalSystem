using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UBBBikeRentalSystem.Models {
    public class Reservation {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Pojazd jest wymagany.")]
        [ForeignKey("Vehicle")]
        public Vehicle VehicleID { get; set; }

        [Required(ErrorMessage = "Użytkownik jest wymagany.")]
        [ForeignKey("User")]
        public User UserID { get; set; }

        public virtual ReservationPoint ReservationPointID { get; set; }

        public virtual ReservationPoint? ReturnPointID { get; set; }

        [Required(ErrorMessage = "Data rezerwacji jest wymagana.")]
        [Display(Name = "Data rezerwacji")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Data zwrotu")]
        public DateTime? ReturnDate { get; set; }
    }
}