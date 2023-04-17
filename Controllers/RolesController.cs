using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Controllers {
    public class RolesController : Controller {
        private readonly ILogger<RolesController> _logger;
        private readonly RoleManager<Role> _roleManager;

        public RolesController(ILogger<RolesController> logger, RoleManager<Role> roleManager) {
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_roleManager.Roles);

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult()) {
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded) return BadRequest(result.Errors);
            }

            return RedirectToAction("Index");
        }
    }
}