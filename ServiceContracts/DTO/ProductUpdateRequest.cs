using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductUpdateRequest
    {
        [Required(ErrorMessage ="ProductID is required to update any product")]
        public int? ProductID { get; set; }
        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int Stock { get; set; }
        public string? SkuID { get; set; }
        public string? MainImagePath { get; set; }
        public List<string>? ReferenceImagePaths { get; set; }
        public int? CategoryID { get; set; }
        public Products ToProduct()
        {
            return new Products() { ProductID = ProductID, ProductPrice = ProductPrice, ProductSalePrice = ProductSalePrice, Stock = Stock, SkuID = SkuID,CategoryID = CategoryID};
        }
    }
}
