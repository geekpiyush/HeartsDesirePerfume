using Microsoft.AspNetCore.Mvc;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        public IActionResult Orders()
        {
            return View();
        }
    }
}
