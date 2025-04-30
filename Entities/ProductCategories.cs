using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductCategories
    {

        [Key]
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public virtual ICollection<Products>? Products { get; set; }
    }
}




