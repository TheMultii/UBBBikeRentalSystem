using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.Validators;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class ReservationController : Controller, ICRUD<ReservationViewModel, int> {

        // make sure to adjust date ranges in views (their inputs)

        private readonly IRepository<Reservation, int> _reservationRepository;
        private readonly IMapper _mapper;
        private readonly ReservationValidator validator;

        public ReservationController(IRepository<Reservation, int> db, IMapper mapper) {
            _reservationRepository = db;
            _mapper = mapper;
            validator = new();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost] // ValidateAntiForgeryToken
        public IActionResult Create(ReservationViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            Reservation reservation = _mapper.Map<Reservation>(_model);
            var result = validator.Validate(reservation);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _reservationRepository.Add(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            Reservation? rp = _reservationRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            ReservationViewModel reservationVM = _mapper.Map<ReservationViewModel>(rp);

            return View(reservationVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(ReservationViewModel _model) {
            _reservationRepository.Delete(_model.ID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id) {
            Reservation? rp = _reservationRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            ReservationViewModel reservationVM = _mapper.Map<ReservationViewModel>(rp);

            return View(reservationVM);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            Reservation? rp = _reservationRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            ReservationViewModel reservationVM = _mapper.Map<ReservationViewModel>(rp);

            return View(reservationVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            Reservation reservation = _mapper.Map<Reservation>(_model);
            var result = validator.Validate(reservation);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _reservationRepository.Update(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<ReservationViewModel> _rpvm = new();

            foreach (var reservation in _reservationRepository.GetAll()) {
                ReservationViewModel reservationVM = _mapper.Map<ReservationViewModel>(reservation);
                _rpvm.Add(reservationVM);
            }

            return View(_rpvm);
        }
    }
}
