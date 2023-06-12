using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.ViewModels {
    public class AreaAdminUsersViewModel {
        public List<UserViewModel> users { get; set; }
        public UserViewModel loggedInUser { get; set; }
    }
}
