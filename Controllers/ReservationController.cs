using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Validators;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class ReservationController : Controller, ICRUD<ReservationViewModel> {

        // make sure to adjust date ranges in views (their inputs)

        private readonly IMapper _mapper;
        private readonly ReservationValidator validator;

        public ReservationController(IMapper mapper) {
            _mapper = mapper;
            validator = new();
        }

        [HttpGet]
        public IActionResult Create() {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Create(ReservationViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            var result = validator.Validate(_mapper.Map<Reservation>(_model));
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            return BadRequest("1");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(ReservationViewModel _model) {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Details(int id) {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationViewModel _model) {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Index() {
            throw new NotImplementedException();
        }
    }
}
