using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductCategoryResponse
    {
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }
    }

    public static class ProductCategoryExtension
    {
        public static ProductCategoryResponse ToProductCategoryResponse(this ProductCategories productCategories)
        {
            return new ProductCategoryResponse() { CategoryID = productCategories.CategoryID, CategoryName = productCategories.CategoryName };
        }
    }
}
