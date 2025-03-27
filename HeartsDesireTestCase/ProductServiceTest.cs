using ServiceContracts;
using Services;

namespace HeartsDesireTestCase
{
    public class ProductServiceTest
    {
        private readonly IProductServices _productServices;

        public ProductServiceTest()
        {
            _productServices = new ProductService();
        }
        [Fact]
        public void AddProduct_NullProduct()
        {

        }
    }
}