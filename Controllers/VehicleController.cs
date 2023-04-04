using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Converters;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class VehicleController: Controller, ICRUD<VehicleDetailViewModel> {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleController(IRepository<Vehicle> db, IRepository<VehicleType> vehicleTypeRepository) {
            _vehicleRepository = db;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VehicleDetailViewModel _model) {
            if (!ModelState.IsValid) return View(_model);

            Vehicle vehicle = new() {
                Name = _model.Name,
                Model = _model.Model,
                ImageUrl = _model.ImageUrl,
                VehicleType = VehicleTypeConverter.ToVehicleType(_model.VehicleType, _vehicleTypeRepository),
                PricePerHour = _model.PricePerHour,
                Description = _model.Description,
                IsAvailable = true,
                ManufactureDate = _model.ManufactureDate,
                MaxSpeed = _model.MaxSpeed
            };

            _vehicleRepository.Add(vehicle);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(new VehicleDetailViewModel() {
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
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(VehicleDetailViewModel vehicle) {
            _vehicleRepository.Delete(vehicle.ID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(new VehicleDetailViewModel() {
                ID = vehicle.ID,
                Name = vehicle?.Name ?? "N/A",
                Model = vehicle?.Model,
                ImageUrl = vehicle?.ImageUrl,
                VehicleType = VehicleTypeConverter.ToVehicleTypeEnum(vehicle?.VehicleType),
                PricePerHour = vehicle?.PricePerHour ?? 0,
                Description = vehicle?.Description ?? "N/A",
                IsAvailable = vehicle?.IsAvailable ?? false,
                ManufactureDate = vehicle?.ManufactureDate ?? DateTime.MinValue,
                MaxSpeed = vehicle?.MaxSpeed ?? 0
            });
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(new VehicleDetailViewModel() {
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
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleDetailViewModel _model) {
            if (!ModelState.IsValid) return View(_model);

            Vehicle vehicle = new() {
                ID = _model.ID,
                Name = _model.Name,
                Model = _model.Model,
                ImageUrl = _model.ImageUrl,
                VehicleType = VehicleTypeConverter.ToVehicleType(_model.VehicleType, _vehicleTypeRepository),
                PricePerHour = _model.PricePerHour,
                IsAvailable = true,
                Description = _model.Description,
                ManufactureDate = _model.ManufactureDate,
                MaxSpeed = _model.MaxSpeed
            };

            _vehicleRepository.Update(vehicle);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<VehicleItemViewModel> vehicles = new();
            foreach (var vehicle in _vehicleRepository.GetAll()) {
                vehicles.Add(new VehicleItemViewModel() {
                    ID = vehicle.ID,
                    Name = vehicle.Name,
                    Model = vehicle.Model,
                    ImageUrl = vehicle.ImageUrl,
                    VehicleType = VehicleTypeConverter.ToVehicleTypeEnum(vehicle.VehicleType),
                    PricePerHour = vehicle.PricePerHour
                });
            }

            return View(vehicles);
        }
    }
}
