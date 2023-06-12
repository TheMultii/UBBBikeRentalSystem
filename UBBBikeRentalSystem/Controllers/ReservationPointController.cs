using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.Validators;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class ReservationPointController: Controller, ICRUD<ReservationPointViewModel, int> {
        private readonly IRepository<ReservationPoint, int> _reservationPointRepository;
        private readonly IMapper _mapper;
        private readonly ReservationPointValidator validator;

        public ReservationPointController(IRepository<ReservationPoint, int> db, IMapper mapper) {
            _reservationPointRepository = db;
            _mapper = mapper;
            validator = new();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ReservationPointViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            ReservationPoint reservation = _mapper.Map<ReservationPoint>(_model);
            var result = validator.Validate(reservation);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _reservationPointRepository.Add(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            ReservationPointViewModel reservationVM = _mapper.Map<ReservationPointViewModel>(rp);

            return View(reservationVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(ReservationPointViewModel vehicle) {
            _reservationPointRepository.Delete(vehicle.ID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            ReservationPointViewModel reservationVM = _mapper.Map<ReservationPointViewModel>(rp);

            return View(reservationVM);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            ReservationPointViewModel reservationVM = _mapper.Map<ReservationPointViewModel>(rp);

            return View(reservationVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationPointViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            ReservationPoint reservation = _mapper.Map<ReservationPoint>(_model);
            var result = validator.Validate(reservation);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _reservationPointRepository.Update(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<ReservationPointViewModel> _rpvm = new();

            foreach (var reservationPoint in _reservationPointRepository.GetAll()) {
                ReservationPointViewModel reservationVM = _mapper.Map<ReservationPointViewModel>(reservationPoint);
                _rpvm.Add(reservationVM);
            }

            return View(_rpvm);
        }
    }
}
