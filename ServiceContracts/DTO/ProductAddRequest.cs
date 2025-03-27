using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace ServiceContracts.DTO
{
    public class ProductAddRequest
    {
       public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int Stock { get; set; }
        public string? SkuID { get; set; }

        public Products ToProducts()
        {
            return new Products() { ProductID = ProductID, ProductName = ProductName, ProductPrice = ProductPrice, ProductSalePrice = ProductSalePrice, SkuID = SkuID, Stock = Stock };
        }

    }
}
