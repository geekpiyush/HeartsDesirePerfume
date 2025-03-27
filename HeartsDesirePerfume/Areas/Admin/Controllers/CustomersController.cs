using Microsoft.AspNetCore.Mvc;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        public IActionResult Customers()
        {
            return View();
        }
    }
}
