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
            // sprawdzanie, czy model przekazany do akcji zawiera poprawne dane (czy przechodzi walidację).
            if (!ModelState.IsValid) return View(_model);

            _vehicleRepository.Add(
                VehicleModelToViewModelConverter.ToVehicle(_model, _vehicleTypeRepository)
            );
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(
                VehicleModelToViewModelConverter.ToVehicleDetailViewModel(vehicle)
            );
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

            return View(
                VehicleModelToViewModelConverter.ToVehicleDetailViewModel(vehicle)
            );
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(
                VehicleModelToViewModelConverter.ToVehicleDetailViewModel(vehicle)
            );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleDetailViewModel _model) {
            if (!ModelState.IsValid) return View(_model);

            _vehicleRepository.Update(
                VehicleModelToViewModelConverter.ToVehicle(_model, _vehicleTypeRepository)
            );
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<VehicleItemViewModel> vehicles = new();
            foreach (var vehicle in _vehicleRepository.GetAll()) {
                vehicles.Add(
                    VehicleModelToViewModelConverter.ToVehicleItemViewModel(vehicle)
                );
            }

            return View(vehicles);
        }
    }
}
