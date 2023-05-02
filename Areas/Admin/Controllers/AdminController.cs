using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.Controllers {
    [Area("Admin")]
    public class AdminController : Controller {
        private readonly IRepository<User, string> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AdminController(IRepository<User, string> db, IMapper mapper, UserManager<User> userManager) {
            _userRepository = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Users() {
            List<UserViewModel> _uvm = new();

            foreach (var user in _userRepository.GetAll()) {
                UserViewModel userVM = _mapper.Map<UserViewModel>(user);

                var _u = await _userManager.FindByIdAsync(user.Id);
                if (_u != null) userVM.Roles = (await _userManager.GetRolesAsync(_u)).ToList();
                _uvm.Add(userVM);
            }

            return View(_uvm);
        }
    }
}
