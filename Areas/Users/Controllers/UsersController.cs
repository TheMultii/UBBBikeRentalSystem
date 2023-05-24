using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace UBBBikeRentalSystem.Areas.Users.Controllers {
    [Area("Users")]
    public class UsersController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
