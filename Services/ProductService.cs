using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

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
           Products products = productAddRequest.ToProducts();

            products.ProductID = Guid.NewGuid();

            _products.Add(products);
            return products.ToProductResponse();
        }
    }
}
