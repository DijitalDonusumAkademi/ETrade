using System.Collections.Generic;
using Entities;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetByIdWithProducts(int id);
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        Category GetById(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}