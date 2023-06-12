using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Users.ViewModels {
    public class MyReservationsViewModel {
        public UserViewModel LoggedInUser { get; set; }
        public List<ReservationViewModel> Reservations { get; set; }
    }
}
