using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.ViewModels {
    public class AreaAdminUserReservationsViewModel {
        public List<ReservationViewModel> reservations { get; set; }
        public UserViewModel loggedInUser { get; set; }
    }
}
