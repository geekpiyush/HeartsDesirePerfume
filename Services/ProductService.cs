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

            products.ProductID = Guid.NewGuid();

            _products.Add(products);

            return products.ToProductResponse();
        }

        public List<ProductResponse> GetAllProducts()
        {
          return _products.Select(temp => temp.ToProductResponse()).ToList(); 
        }

        public ProductResponse GetProductByProductID(Guid? personID)
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
    }
}
