using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.Validators;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Controllers {
    public class UserController: Controller, ICRUD<UserViewModel> {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserValidator validator;

        public UserController(IRepository<User> db, IMapper mapper) {
            _userRepository = db;
            _mapper = mapper;
            validator = new();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            User user = _mapper.Map<User>(_model);
            var result = validator.Validate(user);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _userRepository.Add(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            User? rp = _userRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            UserViewModel userVM = _mapper.Map<UserViewModel>(rp);

            return View(userVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(UserViewModel user) {
            _userRepository.Delete(user.ID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id) {
            User? rp = _userRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            UserViewModel userVM = _mapper.Map<UserViewModel>(rp);

            return View(userVM);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            User? rp = _userRepository.Get(id);
            if (rp is null) return RedirectToAction("Index");
            UserViewModel userVM = _mapper.Map<UserViewModel>(rp);

            return View(userVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel _model) {
            if (!ModelState.IsValid) return BadRequest(ModelState); //check for viewmodel validation
            User reservation = _mapper.Map<User>(_model);
            var result = validator.Validate(reservation);
            if (!result.IsValid) { //check for database model validation
                return BadRequest(result.Errors);
            }

            _userRepository.Update(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index() {
            List<UserViewModel> _uvm = new();

            foreach (var user in _userRepository.GetAll()) {
                UserViewModel userVM = _mapper.Map<UserViewModel>(user);
                _uvm.Add(userVM);
            }

            return View(_uvm);
        }
    }
}
