using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Users.ViewModels {
    public class GET_MakeReservationViewModel {
        public ICollection<VehicleItemViewModel> Vehicles { get; set; }
        public ICollection<ReservationPointViewModel> ReservationPoints { get; set; }
        public UserViewModel LoggedInUser { get; set; }
    }
}
