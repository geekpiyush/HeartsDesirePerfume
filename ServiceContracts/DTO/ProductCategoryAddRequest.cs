using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductCategoryAddRequest
    {

        [Required(ErrorMessage ="Category Name is required")]
        public string? CategoryName { get; set; }


        public ProductCategories ToProductCategories()
        {
            return new ProductCategories() { CategoryName = CategoryName };
        }
    }
}
