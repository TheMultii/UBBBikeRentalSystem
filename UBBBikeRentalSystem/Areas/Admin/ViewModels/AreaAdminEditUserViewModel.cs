using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.ViewModels {
    public class AreaAdminEditUserViewModel {
        public UserWithReservationsViewModel user { get; set; }
        public UserViewModel loggedInUser { get; set; }
    }
}
