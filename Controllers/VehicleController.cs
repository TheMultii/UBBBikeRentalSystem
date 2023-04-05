using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class VehicleController: Controller, ICRUD<VehicleDetailViewModel> {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleController(IRepository<Vehicle> db, IMapper mapper) {
            _vehicleRepository = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VehicleDetailViewModel _model) {
            // sprawdzanie, czy model przekazany do akcji zawiera poprawne dane (czy przechodzi walidację).
            if (!ModelState.IsValid) return View(_model);

            _vehicleRepository.Add(_mapper.Map<Vehicle>(_model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
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

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            Vehicle? vehicle = _vehicleRepository.Get(id);
            if (vehicle is null) return RedirectToAction("Index");

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleDetailViewModel _model) {
            if (!ModelState.IsValid) return View(_model);
			
            _vehicleRepository.Update(_mapper.Map<Vehicle>(_model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<VehicleItemViewModel> vehicles = new();
            foreach (var vehicle in _vehicleRepository.GetAll()) {
                vehicles.Add(_mapper.Map<VehicleItemViewModel>(vehicle));
            }

            return View(vehicles);
        }
    }
}
