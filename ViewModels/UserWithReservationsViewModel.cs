using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.ViewModels {
    public class UserWithReservationsViewModel: UserViewModel {
        public List<ReservationViewModel> Reservations { get; set; }
    }
}
