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
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }

        public int Stock { get; set; }
        public string? SkuID { get; set; }

    }
    public static class ProductExtensions
    {
        public static ProductResponse ToProductResponse(this Products products)
        {
            return new ProductResponse() { ProductID = products.ProductID, ProductName = products.ProductName, ProductPrice = products.ProductPrice, ProductSalePrice = products.ProductSalePrice, SkuID = products.SkuID, Stock = products.Stock };
        }
    }
}
