using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class ProductDal : Repository<Product,AppDbContext>,IProductDal
    {
        public IEnumerable<Product> GetPopularProduct()
        {
            throw new System.NotImplementedException();
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Products.Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefault();
            }
        }

        public List<Product> GetProductByCategory(string category, int page, int pagesize)
        {
            using (var context = new AppDbContext())
            {
                var products = context.Products.AsQueryable();
                if (string.IsNullOrEmpty(category))
                {
                    products = products.Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category).Where(x =>
                            x.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page - 1) * pagesize).Take(pagesize).ToList(); // sayfalama
            }
        }

        public int GetProductsByCategory(string category)
        {
            using (var context = new AppDbContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category).Where(x =>
                            x.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Count();
            }
        }

        public Product GetByIdWithCategories(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Products.Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new AppDbContext())
            {
                var product = context.Products.Include(i => i.ProductCategories).FirstOrDefault(x => x.Id == entity.Id);

                product.Name = entity.Name;
                product.ImageUrl = entity.ImageUrl;
                product.Price = entity.Price;
                product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                {
                    CategoryId = catid,
                    ProductId = entity.Id
                }).ToList();
                context.SaveChanges();
            }
        }
    }
}