using Entities;
using Entities.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class ProductService : IProductServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductCategoryService _productCategoryService;
        public ProductService( ApplicationDbContext applicationDbContext, IProductCategoryService productCategoryService)
        {

            _db = applicationDbContext;
            _productCategoryService = productCategoryService;
        }

        private ProductResponse ConvertProductToProductResponse(Products products)
        {
            ProductResponse productResponse = products.ToProductResponse();
            productResponse.Category = _productCategoryService.GetCategoryByCategoryID(productResponse.CategoryID)?.CategoryName;

            return productResponse;
        }
        public ProductResponse AddProduct(ProductAddRequest? productAddRequest)
        {
            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            ValidationHelper.ModelValidation(productAddRequest);

            Products product = productAddRequest.ToProducts();

            // Save Main Image in "MainImages" folder
            if (productAddRequest.MainImage != null)
            {
                string mainImagePath = SaveImage(productAddRequest.MainImage, "MainImages");
                product.MainImagePath = mainImagePath;
            }

            // Save Reference Images in "ReferenceImages" folder
            if (productAddRequest.ReferenceImages != null && productAddRequest.ReferenceImages.Count > 0)
            {
                List<string> imagePaths = new List<string>();
                foreach (var image in productAddRequest.ReferenceImages)
                {
                    string path = SaveImage(image, "ReferenceImages");
                    imagePaths.Add(path);
                }

                product.ReferenceImages = string.Join("\n", imagePaths); 
            }

            ProductResponse productResponse =  product.ToProductResponse();
              productResponse.Category = _productCategoryService.GetCategoryByCategoryID(productResponse.CategoryID)?.CategoryName;

            _db.Products.Add(product);
            _db.SaveChanges();

            return ConvertProductToProductResponse(product);
        }


        private string SaveImage(IFormFile image, string folderName)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/{folderName}");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"/uploads/{folderName}/{uniqueFileName}"; 
        }

        public List<ProductResponse> GetAllProducts()
        {
          return _db.Products
                  .Include(p => p.ProductCategory)
                .Select(temp => temp.ToProductResponse())
                .ToList(); 
        }

        public ProductResponse GetProductByProductID(int? productID)
        {
            if(productID == null)
            {
                return null;
            }
           Products? products =  _db.Products.FirstOrDefault(temp => temp.ProductID == productID);

            if(products == null)
            {
                return null;
            }
            return products.ToProductResponse();
        }

        public ProductResponse UpdateProduct(ProductUpdateRequest? productUpdateRequest)
        {
            if(productUpdateRequest == null)
            {
                return null;
            }

            ValidationHelper.ModelValidation(productUpdateRequest);

            Products? matchingProducts = _db.Products.FirstOrDefault(temp => temp.ProductID == productUpdateRequest.ProductID);

            if(matchingProducts == null)
            {
                throw new ArgumentException("Given ID doesn't exist");
            }

            //Update Product Details
            matchingProducts.ProductPrice = productUpdateRequest.ProductPrice;
            matchingProducts.ProductSalePrice = productUpdateRequest.ProductSalePrice;
            matchingProducts.SkuID = productUpdateRequest.SkuID;
            matchingProducts.Stock = productUpdateRequest.Stock;
            matchingProducts.MainImagePath = productUpdateRequest.MainImagePath;
            matchingProducts.CategoryID = productUpdateRequest.CategoryID;
            

            _db.SaveChanges();
            return matchingProducts.ToProductResponse();
        }

        public bool DeleteProduct(int? productID)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            Products? product = _db.Products.FirstOrDefault(temp => temp.ProductID == productID);

            if(product == null)
            {
                return false;
            }

            _db.Products.Remove(_db.Products.First( temp => temp.ProductID == productID));
            _db.SaveChanges();

            return true;

        }

        // get product by productCategoryID
        public List<ProductResponse> GetProductsByCategoryID(int categoryID)
        {
            var products = _db.Products
         .Include(p => p.ProductCategory)
         .Where(p => p.CategoryID == categoryID)
         .Select(p => p.ToProductResponse())
         .ToList();

            return products;
        }
    }
}
