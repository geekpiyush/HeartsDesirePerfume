using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
   public class ProductResponse
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }

        public int Stock { get; set; }
        public string? SkuID { get; set; }

        public string? MainImagePath { get; set; } 
        public List<string>? ReferenceImagePaths { get; set; }

        public ProductUpdateRequest ToProductUpdateRequest()
        {
            return new ProductUpdateRequest() { ProductID = ProductID, ProductPrice = ProductPrice, ProductSalePrice = ProductSalePrice, SkuID = SkuID, Stock = Stock};
        }
    }
    public static class ProductExtensions
    {
        public static ProductResponse ToProductResponse(this Products products)
        {
            return new ProductResponse() { ProductID = products.ProductID, ProductName = products.ProductName, ProductPrice = products.ProductPrice, ProductSalePrice = products.ProductSalePrice, SkuID = products.SkuID, Stock = products.Stock, Description = products.Description, ShortDescription = products.ShortDescription,MainImagePath = products.MainImagePath,
                ReferenceImagePaths = string.IsNullOrEmpty(products.ReferenceImages)
                ? new List<string>()
                : products.ReferenceImages.Split('\n').ToList()
            };
        }
    }
}
