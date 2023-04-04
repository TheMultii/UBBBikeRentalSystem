using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels;

public class VehicleDetailViewModel : VehicleBase {
    //wszystko, co można napisać o pojeździe
    [Display(Name = "Opis")]
    public string? Description { get; set; }
    [Display(Name = "Dostępność")]
    public bool IsAvailable { get; set; }
    [Display(Name = "Data produkcji")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime ManufactureDate { get; set; }
    [Display(Name = "Prędkość maksymalna")]
    [DisplayFormat(DataFormatString = "{0} km/h")]
    public double? MaxSpeed { get; set; }
}
