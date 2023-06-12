using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Converters {
    public static class VehicleTypeConverter {
        public static VehicleType? ToVehicleType(VehicleTypeEnum? vehicleType, IRepository<VehicleType, int> db) {
            return db.RawQueryable().Where(x => x.Name == vehicleType.ToString()).FirstOrDefault();
        }

        public static VehicleType? ToVehicleType(VehicleTypeEnum? vehicleType, UBBBikeRentalSystemDatabase db) {
            return db.VehicleTypes.Where(x => x.Name == vehicleType.ToString()).FirstOrDefault();
        }

        public static VehicleTypeEnum ToVehicleTypeEnum(VehicleType? vehicleType) {
            if (vehicleType == null) return VehicleTypeEnum.Bike;
            return vehicleType switch {
                { Name: "Bike" } => VehicleTypeEnum.Bike,
                { Name: "Car" } => VehicleTypeEnum.Car,
                { Name: "Scooter" } => VehicleTypeEnum.Scooter,
                _ => VehicleTypeEnum.Bike
            };
        }
    }
}
