using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace ServiceContracts.DTO
{
    public class ProductAddRequest
    {
        [Required(ErrorMessage ="ProductID can't be blank")]
       public Guid ProductID { get; set; }

        [Required(ErrorMessage = "Product Name can't be blank")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Product Price can't be blank")]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "Product Sale Price can't be blank")]
        public double? ProductSalePrice { get; set; }

        [Required(ErrorMessage = "Product Stock can't be blank")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Product SKUID can't be blank")]
        public string? SkuID { get; set; }

        public Products ToProducts()
        {
            return new Products() { ProductID = ProductID, ProductName = ProductName, ProductPrice = ProductPrice, ProductSalePrice = ProductSalePrice, SkuID = SkuID, Stock = Stock };
        }

    }
}
