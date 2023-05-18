using Microsoft.AspNetCore.Mvc;

namespace UBBBikeRentalSystem.Areas.Users.Controllers {
    public class UsersController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
