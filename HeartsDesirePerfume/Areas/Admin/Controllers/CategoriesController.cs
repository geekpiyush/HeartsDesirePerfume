using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        public CategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public IActionResult Categories()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCategories()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddCategories(ProductCategoryAddRequest productCategoryAddRequest)
        {
            ProductCategoryResponse productCategoryResponse =  _productCategoryService.AddCategory(productCategoryAddRequest);

            return RedirectToAction("Categories", "Categories");
        }
    }
}
