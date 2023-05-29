using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.ViewModels {
    public class GET_AreaAdminEditUserReservationViewModel {
        public ReservationViewModel reservation { get; set; }
        public UserViewModel loggedInUser { get; set; }
        public List<ReservationPointViewModel> reservationPoints { get; set; }
    }
}
