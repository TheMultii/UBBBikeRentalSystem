using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.Models {
    public class ReservationPoint {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
