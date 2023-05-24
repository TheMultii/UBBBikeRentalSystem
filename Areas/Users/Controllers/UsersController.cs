using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Users.Controllers {
    [Area("Users")]
    public class UsersController : Controller {
        private readonly IRepository<User, string> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(IRepository<User, string> uDB, UserManager<User> userManager, IMapper mapper) {
            _userRepository = uDB;
            _userManager = userManager;
            _mapper = mapper;
        }

        private async Task<User?> GetLoggedInUser() {
            if (User?.Identity?.IsAuthenticated ?? false) {
                try {
                    return await _userManager.FindByNameAsync(User?.Identity?.Name ?? "");
                } catch {
                    return null;
                }
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            User? loggedInUser = await GetLoggedInUser();
            if (loggedInUser == null) return Forbid();
            UserViewModel userVM = _mapper.Map<UserViewModel>(loggedInUser);

            return View(userVM);
        }
    }
}
