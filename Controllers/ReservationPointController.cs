using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Converters;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class ReservationPointController: Controller, ICRUD<ReservationPointViewModel> {
        private readonly IRepository<ReservationPoint> _reservationPointRepository;

        //merge into one helper class (creating model)

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

            ReservationPoint rp = new() {
                ID = _model.ID,
                Name = _model.Name,
                Address = _model.Address,
                City = _model.City,
                PostalCode = _model.PostalCode,
                Longitude = _model.Longitude,
                Latitude = _model.Latitude,
            };

            _reservationPointRepository.Add(rp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");

            ReservationPointViewModel rpvm = new() {
                ID = rp.ID,
                Name = rp.Name,
                Address = rp.Address,
                City = rp.City,
                PostalCode = rp.PostalCode,
                Longitude = rp.Longitude,
                Latitude = rp.Latitude,
            };

            return View(rpvm);
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

            return View(new ReservationPointViewModel() {
                ID = rp.ID,
                Name = rp.Name,
                Address = rp.Address,
                City = rp.City,
                PostalCode = rp.PostalCode,
                Longitude = rp.Longitude,
                Latitude = rp.Latitude,
            });
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            ReservationPoint? rp = _reservationPointRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");

            return View(new ReservationPointViewModel() {
                ID = rp.ID,
                Name = rp.Name,
                Address = rp.Address,
                City = rp.City,
                PostalCode = rp.PostalCode,
                Longitude = rp.Longitude,
                Latitude = rp.Latitude,
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationPointViewModel _model) {
            if (!ModelState.IsValid) return View(_model);

            ReservationPoint rp = new() {
                ID = _model.ID,
                Name = _model.Name,
                Address = _model.Address,
                City = _model.City,
                PostalCode = _model.PostalCode,
                Longitude = _model.Longitude,
                Latitude = _model.Latitude,
            };

            _reservationPointRepository.Update(rp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<ReservationPointViewModel> _rpvm = new() { };

            foreach (var item in _reservationPointRepository.GetAll()) {
                _rpvm.Add(new ReservationPointViewModel() {
                    ID = item.ID,
                    Name = item.Name,
                    Address = item.Address,
                    City = item.City,
                    PostalCode = item.PostalCode,
                    Longitude = item.Longitude,
                    Latitude = item.Latitude,
                });
            }

            return View(_rpvm);
        }
    }
}
