using Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class ProductService : IProductServices
    {
        private readonly List<Products> _products;
        public ProductService()
        {
            _products = new List<Products>();
        }
        public ProductResponse AddProduct(ProductAddRequest? productAddRequest)
        {
            if(productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            ValidationHelper.ModelValidation(productAddRequest);

           Products products = productAddRequest.ToProducts();

            //products.ProductID = Guid.NewGuid();

            _products.Add(products);

            return products.ToProductResponse();
        }

      

        public List<ProductResponse> GetAllProducts()
        {
          return _products.Select(temp => temp.ToProductResponse()).ToList(); 
        }

        public ProductResponse GetProductByProductID(int? personID)
        {
            if(personID == null)
            {
                return null;
            }
           Products? products =  _products.FirstOrDefault(temp => temp.ProductID == personID);

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

            Products? matchingProducts = _products.FirstOrDefault(temp => temp.ProductID == productUpdateRequest.ProductID);

            if(matchingProducts == null)
            {
                throw new ArgumentException("Given ID doesn't exist");
            }

            //Update Product Details
            matchingProducts.ProductPrice = productUpdateRequest.ProductPrice;
            matchingProducts.ProductSalePrice = productUpdateRequest.ProductSalePrice;
            matchingProducts.SkuID = productUpdateRequest.SkuID;
            matchingProducts.Stock = productUpdateRequest.Stock;


            return matchingProducts.ToProductResponse();
        }

        public bool DeleteProduct(int? productID)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            Products? product = _products.FirstOrDefault(temp => temp.ProductID == productID);

            if(product == null)
            {
                return false;
            }

            _products.RemoveAll(temp => temp.ProductID == productID);

            return true;

        }
    }
}
