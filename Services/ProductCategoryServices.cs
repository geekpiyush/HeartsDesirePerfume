using Entities;
using Entities.DB;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductCategoryServices : IProductCategoryService
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryServices(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }
        public ProductCategoryResponse AddCategory(ProductCategoryAddRequest? productCategoryAddRequest)
        {
            if (productCategoryAddRequest == null)
                throw new ArgumentNullException(nameof(productCategoryAddRequest));

            if(productCategoryAddRequest.CategoryName == null)
            {
                throw new ArgumentException(productCategoryAddRequest.CategoryName);
            }
            
            if(_db.ProductCategories.Where(temp => temp.CategoryName == productCategoryAddRequest.CategoryName).Count()>0)
            {
                throw new ArgumentException("Category Name Already Exists");
            }

            ProductCategories productCategories = productCategoryAddRequest.ToProductCategories();
            
            _db.ProductCategories.Add(productCategories);
            _db.SaveChanges();

            return productCategories.ToProductCategoryResponse();
        }

        public List<ProductCategoryResponse> GetAllCategory()
        {
             return _db.ProductCategories.Select(temp => temp.ToProductCategoryResponse()).ToList();
        }

        public ProductCategoryResponse GetCategoryByCategoryID(int? CategoryID)
        {
            if (CategoryID == null)
                return null;
            

             ProductCategories? productCategory = _db.ProductCategories.FirstOrDefault(temp => temp.CategoryID == CategoryID);

            return productCategory?.ToProductCategoryResponse();
        }
    }
}
