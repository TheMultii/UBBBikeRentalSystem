using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.Validators;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class VehicleController: Controller, ICRUD<VehicleDetailViewModel, int> {
        private readonly IRepository<Vehicle, int> _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly VehicleValidator validator;

        public VehicleController(IRepository<Vehicle, int> db, IMapper mapper) {
            _vehicleRepository = db;
            _mapper = mapper;
            validator = new();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VehicleDetailViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            Vehicle vehicle = _mapper.Map<Vehicle>(_model);
            var result = validator.Validate(vehicle);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _vehicleRepository.Add(vehicle);
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
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            Vehicle vehicle = _mapper.Map<Vehicle>(_model);
            var result = validator.Validate(vehicle);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _vehicleRepository.Update(vehicle);
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
