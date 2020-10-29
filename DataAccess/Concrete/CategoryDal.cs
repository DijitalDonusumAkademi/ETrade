using System.Linq;
using System.Security.Principal;
using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DataAccess.Concrete
{
    public class CategoryDal : Repository<Category,AppDbContext>,ICategoryDal
    {
        public Category GetByIdWithProducts(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Categories.Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(i => i.Product).FirstOrDefault();
            }
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new AppDbContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlCommand(cmd, productId, categoryId);
            }
        }
    }
}