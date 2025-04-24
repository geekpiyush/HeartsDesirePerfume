using Entities.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomersController(UserManager<ApplicationUser> user)
        {
            _userManager = user;
        }

        public IActionResult Customers()
        {
           var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}
