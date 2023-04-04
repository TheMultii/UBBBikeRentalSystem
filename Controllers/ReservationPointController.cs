using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(ReservationPointViewModel vehicle) {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id) {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationPointViewModel _model) {
            return View();
        }

        [HttpGet]
        public IActionResult Index() {
            return View(new List<ReservationPointViewModel>());
        }
    }
}
