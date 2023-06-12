using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Controllers {
    public class AccountController : Controller {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager) {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            User? user = null;
            if (User?.Identity?.IsAuthenticated ?? false) {
                try {
                    user = await _userManager.FindByNameAsync(User?.Identity?.Name ?? "");
                } catch (Exception e) {
                    _logger.LogError(e, "Failed to get user");
                }
            }
            return View(user);
        }
    }
}