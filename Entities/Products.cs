using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class Products
    {
        [Key]
        public int? ProductID { get; set; }

        [StringLength(100)]
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }

        [StringLength(255)]
        public string? ShortDescription { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public int Stock { get; set; }
        public string? SkuID { get; set; }
        public string? MainImagePath { get; set; }
        public string? ReferenceImages { get; set; }
        public int? CategoryID { get; set; }
        public virtual ProductCategories? ProductCategory { get; set; }

    }
}
 