using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Areas.Users.ViewModels;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Areas.Users.Controllers {
    [Area("Users"), Authorize(Roles = "Użytkownik")]
    public class UsersController : Controller {
        private readonly IRepository<Vehicle, int> _vehicleRepository;
        private readonly IRepository<ReservationPoint, int> _reservationPointRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(IRepository<Vehicle, int> vDB, IRepository<ReservationPoint, int> rpDB, UserManager<User> userManager, IMapper mapper, UBBBikeRentalSystemDatabase db) {
            _vehicleRepository = vDB;
            _reservationPointRepository = rpDB;
            _reservationRepository = new(db);
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

            List<Reservation> reservations = _reservationRepository.GetUsers(loggedInUser.Id);
            List<ReservationViewModel> reservationVMs = new();

            foreach (Reservation reservation in reservations) {
                reservationVMs.Add(_mapper.Map<ReservationViewModel>(reservation));
            }

            MyReservationsViewModel myReservationVM = new() {
                LoggedInUser = userVM,
                Reservations = reservationVMs
            };

            return View(myReservationVM);
        }

        [HttpGet]
        public async Task<IActionResult> MakeReservation() {
            User? loggedInUser = await GetLoggedInUser();
            if (loggedInUser == null) return Forbid();

            UserViewModel userVM = _mapper.Map<UserViewModel>(loggedInUser);

            List<Vehicle> vehicles = _vehicleRepository.GetAll();
            List<VehicleItemViewModel> vehicleVMs = new();

            foreach (Vehicle vehicle in vehicles) {
                vehicleVMs.Add(_mapper.Map<VehicleItemViewModel>(vehicle));
            }

            List<ReservationPoint> reservationPoints = _reservationPointRepository.GetAll();
            List<ReservationPointViewModel> reservationPointVMs = new();

            foreach (ReservationPoint reservationPoint in reservationPoints) {
                reservationPointVMs.Add(_mapper.Map<ReservationPointViewModel>(reservationPoint));
            }

            GET_MakeReservationViewModel r_vm = new() {
                LoggedInUser = userVM,
                Vehicles = vehicleVMs,
                ReservationPoints = reservationPointVMs
            };

            return View(r_vm);
        }

        [HttpPost]
        public IActionResult MakeReservation(POST_MakeReservationViewModel postModel) {
            Vehicle selectedVehicle = _vehicleRepository.Get(postModel.SelectedVehicle) ?? throw new Exception("Brak takiego pojazdu w DB");
            ReservationPoint selectedReservationPoint = _reservationPointRepository.Get(postModel.SelectedReservationPoint) ?? throw new Exception("Brak takiego punktu w DB");

            Reservation reservation = new() {
                VehicleID = selectedVehicle,
                ReservationPoint = selectedReservationPoint,
                ReservationDate = postModel.SelectedReservationDate,
                ReservationStatus = ReservationStatusEnum.NewReservation,
                UserID = _userManager.FindByNameAsync(User?.Identity?.Name ?? "").Result ?? throw new Exception("Brak takiego użytkownika w DB")
            };

            _reservationRepository.Add(reservation);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteReservation(string id) {
            Reservation? reservation = _reservationRepository.Get(id);
            if (reservation == null) return NotFound();

            if (reservation.ReservationStatus == ReservationStatusEnum.NewReservation)
                _reservationRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
