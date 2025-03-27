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
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int Stock { get; set; }
        public string? SkuID { get; set; }

    }
}
