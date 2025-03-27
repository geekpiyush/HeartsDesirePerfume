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
            if(productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            if(productAddRequest.ProductName == null)
            {
                throw new ArgumentException(nameof(productAddRequest.ProductName));
            }

            if(_products.Where(temp => temp.ProductName == productAddRequest.ProductName).Count()>0)
            {
                throw new ArgumentException("Duplicate Product Name");
            }

           Products products = productAddRequest.ToProducts();

            products.ProductID = Guid.NewGuid();

            _products.Add(products);

            return products.ToProductResponse();
        }

        public List<ProductResponse> GetAllProducts()
        {
          return _products.Select(temp => temp.ToProductResponse()).ToList(); 
        }
    }
}
