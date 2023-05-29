using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.ViewModels {
    public class POST_AreaAdminEditUserReservationViewModel {
        public bool isApproved { get; set; }
        public int SelectedReturnPoint { get; set; }
        public DateTime SelectedReturnDate { get; set; }
    }
}
