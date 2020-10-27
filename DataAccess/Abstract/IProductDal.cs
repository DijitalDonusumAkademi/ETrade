using System.Collections;
using System.Collections.Generic;
using Entities;

namespace DataAccess.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        IEnumerable<Product> GetPopularProduct();
        Product GetProductDetails(int id);
        List<Product> GetProductByCategory(string category, int page, int pagesize);
        int GetProductsByCategory(string category);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}