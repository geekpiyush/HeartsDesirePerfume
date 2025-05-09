using HeartsDesireLuxury.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Diagnostics;

namespace HeartsDesireLuxury.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;

        public HomeController( IProductServices productService)
        {
            _productServices = productService;
        }

        public IActionResult Index()
        {
            List<ProductResponse> products = _productServices.GetTopProducts(4);

            return View(products);
        }
       

    }
}
