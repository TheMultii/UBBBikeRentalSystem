using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Converters;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class ReservationPointController: Controller, ICRUD<ReservationPointViewModel> {
        private readonly IRepository<ReservationPoint> _reservationPointRepository;

        public ReservationPointController(IRepository<ReservationPoint> db) {
            _reservationPointRepository = db;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ReservationPointViewModel _model) {
            if (!ModelState.IsValid) return View(_model);

            _reservationPointRepository.Add(
                ReservationPointModelToViewModelConverter.ToReservationPoint(_model)
            );
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");

            return View(
                ReservationPointModelToViewModelConverter.ToReservationPointViewModel(rp)
            );
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

            return View(
                ReservationPointModelToViewModelConverter.ToReservationPointViewModel(rp)
            );
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");

            return View(
                ReservationPointModelToViewModelConverter.ToReservationPointViewModel(rp)
            );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationPointViewModel _model) {
            if (!ModelState.IsValid) return View(_model);

            _reservationPointRepository.Update(
                ReservationPointModelToViewModelConverter.ToReservationPoint(_model)
            );
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<ReservationPointViewModel> _rpvm = new();

            foreach (var item in _reservationPointRepository.GetAll()) {
                _rpvm.Add(
                    ReservationPointModelToViewModelConverter.ToReservationPointViewModel(item)
                );
            }

            return View(_rpvm);
        }
    }
}
