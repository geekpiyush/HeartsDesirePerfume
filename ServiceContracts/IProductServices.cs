using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IProductServices
    {
        ProductResponse AddProduct(ProductAddRequest? productAddRequest);

        List<ProductResponse> GetAllProducts();

        ProductResponse GetProductByProductID(Guid? personID);
    }
}
