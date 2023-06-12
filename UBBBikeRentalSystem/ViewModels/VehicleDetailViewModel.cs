using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public class VehicleDetailViewModel : VehicleBase {
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Display(Name = "Dostępność")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Data produkcji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required(ErrorMessage = "Data produkcji jest wymagana.")]
        [DataType(DataType.Date, ErrorMessage = "Wartość musi być datą.")]
        public DateTime ManufactureDate { get; set; }

        [Display(Name = "Prędkość maksymalna")]
        [DisplayFormat(DataFormatString = "{0} km/h")]
        [Range(1, double.MaxValue, ErrorMessage = "Prędkość maksymalna musi być większa niż 0.")]
        public double? MaxSpeed { get; set; }
    }
}
