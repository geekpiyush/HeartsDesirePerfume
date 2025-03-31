using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductServices _productService;
        public ProductController(IProductServices productServices)
        {
            _productService = productServices;
        }
        public IActionResult AllProduct()
        {
            List<ProductResponse> products =  _productService.GetAllProducts();

            return View(products);
        
        }

        [HttpGet]
          public IActionResult AddProduct()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductAddRequest productAddRequest)
        {
           if(!ModelState.IsValid)
            {
                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View();
            }

           ProductResponse productResponse = _productService.AddProduct(productAddRequest); 

            return RedirectToAction("AllProduct","Product");
        }

        [HttpGet]
        [Route("[action]/{productID}")]
        public IActionResult EditProduct(int productID)
        {
            ProductResponse productResponse = _productService.GetProductByProductID(productID);
            
            if(productResponse ==null)
            {
                return RedirectToAction("AllProduct", "Product");
            }

            ProductUpdateRequest productUpdateRequest = productResponse.ToProductUpdateRequest();

            return View(productUpdateRequest);
        }


        [HttpPost]
        [Route("[action]/{productID}")]
        public IActionResult EditProduct(ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse productResponse = _productService.GetProductByProductID(productUpdateRequest.ProductID);

            if(productResponse == null)
            {
                return RedirectToAction("AllProdcut");
            }

            if(ModelState.IsValid)
            {
                ProductResponse updateProduct = _productService.UpdateProduct(productUpdateRequest);

                return RedirectToAction("AllProduct");
            }
            else
            {
                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View(productResponse.ToProductUpdateRequest());
            }

        }

        [HttpGet]
        [Route("[action]/{productID}")]
        public IActionResult DeleteProduct(int? productID)
        {
            ProductResponse productResponse = _productService.GetProductByProductID(productID);

            if(productResponse == null)
            {
                return RedirectToAction("AllProduct");
            }

            return View(productResponse);
        }


        [HttpPost]
        [Route("[action]/{productID}")]
        public IActionResult DeleteProduct(ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse productResponse = _productService.GetProductByProductID(productUpdateRequest.ProductID);

            if(productResponse == null)
            {
                return RedirectToAction("AllProduct");
            }

            _productService.DeleteProduct(productUpdateRequest.ProductID);

            return RedirectToAction("AllProduct");
        }
    }
}
