using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Converters {
    public static class VehicleModelToViewModelConverter {
        public static Vehicle ToVehicle(VehicleDetailViewModel vehicle, IRepository<VehicleType> vehicleTypeRepository) {
            return new Vehicle() {
                Name = vehicle.Name,
                Model = vehicle.Model,
                ImageUrl = vehicle.ImageUrl,
                VehicleType = VehicleTypeConverter.ToVehicleType(vehicle.VehicleType, vehicleTypeRepository),
                PricePerHour = vehicle.PricePerHour,
                Description = vehicle.Description,
                IsAvailable = true,
                ManufactureDate = vehicle.ManufactureDate,
                MaxSpeed = vehicle.MaxSpeed
            };
        }

        public static VehicleDetailViewModel ToVehicleDetailViewModel(Vehicle vehicle) {
            return new VehicleDetailViewModel() {
                ID = vehicle.ID,
                Name = vehicle?.Name ?? "N/A",
                Model = vehicle?.Model,
                ImageUrl = vehicle?.ImageUrl,
                VehicleType = VehicleTypeConverter.ToVehicleTypeEnum(vehicle?.VehicleType),
                Description = vehicle?.Description ?? "N/A",
                PricePerHour = vehicle?.PricePerHour ?? 0,
                IsAvailable = vehicle?.IsAvailable ?? false,
                ManufactureDate = vehicle?.ManufactureDate ?? DateTime.MinValue,
                MaxSpeed = vehicle?.MaxSpeed ?? 0
            };
        }

        public static VehicleItemViewModel ToVehicleItemViewModel(Vehicle vehicle) {
            return new VehicleItemViewModel() {
                ID = vehicle.ID,
                Name = vehicle.Name,
                Model = vehicle.Model,
                ImageUrl = vehicle.ImageUrl,
                VehicleType = VehicleTypeConverter.ToVehicleTypeEnum(vehicle.VehicleType),
                PricePerHour = vehicle.PricePerHour
            };
        }
    }
}
