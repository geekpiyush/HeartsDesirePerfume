using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductCategoryService
    {
        ProductCategoryResponse AddCategory(ProductCategoryAddRequest? productCategoryAddRequest);

        List<ProductCategoryResponse> GetAllCategory();
        ProductCategoryResponse GetCategoryByCategoryID(int? CategoryID);
    }
}
