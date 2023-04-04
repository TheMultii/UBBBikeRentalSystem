using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public abstract class VehicleBase {
        public int ID { get; set; }
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }
        public string? Model { get; set; }
        [Display(Name = "URL Grafiki")]
        public string? ImageUrl { get; set; }
        [Display(Name = "Typ pojazdu")]
        public VehicleTypeEnum? VehicleType { get; set; }
        [Display(Name = "Cena za godzinę")]
        [DisplayFormat(DataFormatString = "{0} zł/h")]
        public int PricePerHour { get; set; }
    }
}
