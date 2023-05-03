using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Areas.Admin.ViewModels;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Admin.Controllers {
    [Area("Admin"), Authorize(Roles = "Administrator")]
    public class AdminController : Controller {
        private readonly IRepository<User, string> _userRepository;
        private readonly IRepository<Reservation, int> _reservationRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AdminController(IRepository<User, string> uDB, IRepository<Reservation, int> rDB, IMapper mapper, UserManager<User> userManager) {
            _userRepository = uDB;
            _reservationRepository = rDB;
            _mapper = mapper;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> Users() {
            User? loggedInUser = await GetLoggedInUser();
            if (loggedInUser == null) return Forbid();

            List<UserViewModel> _uvm = new();

            foreach (var user in _userRepository.GetAll()) {
                UserViewModel userVM = _mapper.Map<UserViewModel>(user);

                var _u = await _userManager.FindByIdAsync(user.Id);
                if (_u != null) userVM.Roles = (await _userManager.GetRolesAsync(_u)).ToList();
                _uvm.Add(userVM);
            }

            AreaAdminUsersViewModel _auvm = new() {
                users = _uvm,
                loggedInUser = _mapper.Map<UserViewModel>(loggedInUser)
            };

            return View(_auvm);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(Guid id) {
            User? loggedInUser = await GetLoggedInUser();
            if (loggedInUser == null) return Forbid();

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            UserWithReservationsViewModel userVM = _mapper.Map<UserWithReservationsViewModel>(user);
            userVM.Roles = (await _userManager.GetRolesAsync(user)).ToList();

            List<Reservation> reservations = _reservationRepository.RawQueryable()
                .Where(r => r.UserID == user)
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .Include(r => r.ReturnPoint)
                .Where(r => r.UserID == user)
                .OrderBy(r => r.ID)
                .ToList();

            List<ReservationViewModel> reservationVMs = new();
            foreach (var reservation in reservations) {
                ReservationViewModel reservationVM = _mapper.Map<ReservationViewModel>(reservation);
                reservationVMs.Add(reservationVM);
            }

            userVM.Reservations = reservationVMs;

            AreaAdminEditUserViewModel _auvm = new() {
                user = userVM,
                loggedInUser = _mapper.Map<UserViewModel>(loggedInUser)
            };

            return View(_auvm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(Guid id, UserAddRoles addRoles) {
            var roles = addRoles.roles;
            if (roles == null) return BadRequest();
            if (roles.Count == 0) return BadRequest();

            for (int i = 0; i < roles.Count; i++) {
                roles[i] = roles[i].ToLower();
            }

            List<string> validRoles = new() { "administrator", "operator", "użytkownik" };

            foreach (var role in roles) {
                if(!validRoles.Contains(role)) return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            //remove all roles from user
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            //add new roles to user
            foreach (var orderRole in validRoles) {
                if (!addRoles.roles.Contains(orderRole)) continue;
                await _userManager.AddToRoleAsync(user, orderRole);
            }

            return RedirectToAction("Users");
        }
    }
}
