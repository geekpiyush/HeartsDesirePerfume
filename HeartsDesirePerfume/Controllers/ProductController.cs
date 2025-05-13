using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace HeartsDesirePerfume.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productService)
        {
            _productServices = productService;
        }
        public IActionResult AllProducts()
        {
           List<ProductResponse> products = _productServices.GetAllProducts();
            return View(products);
        }
        public IActionResult Women()
        {
            List<ProductResponse> products = _productServices.GetProductsByCategoryID(2);
            return View(products);
        }
        public IActionResult Men()
        {
            List<ProductResponse> products =  _productServices.GetProductsByCategoryID(1);
            return View(products);
        }
        public IActionResult Luxury()
        {
            List<ProductResponse> products = _productServices.GetProductsByCategoryID(4);
            return View(products);
        }

        //fetch product accoridng to there ID
        public IActionResult ProductByProductID(int productID)
        {
           var products = _productServices.GetProductByProductID(productID);   

            return View(products);
        }

    }
}
