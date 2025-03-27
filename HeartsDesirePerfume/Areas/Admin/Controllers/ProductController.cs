using Microsoft.AspNetCore.Mvc;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult AllProduct()
        {
            return View();
        
        }

          public IActionResult AddProduct()
        {
            return View();
        }
    }
}
