using Microsoft.AspNetCore.Mvc;

namespace HeartsDesireLuxury.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
