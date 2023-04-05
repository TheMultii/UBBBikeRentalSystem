using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Converters {
    public static class ReservationPointModelToViewModelConverter {
        public static ReservationPoint ToReservationPoint(ReservationPointViewModel model) {
            return new ReservationPoint() {
                ID = model.ID,
                Name = model.Name,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
            };
        }

        public static ReservationPointViewModel ToReservationPointViewModel(ReservationPoint model) {
            return new ReservationPointViewModel() {
                ID = model.ID,
                Name = model.Name,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
            };
        }
    }
}
