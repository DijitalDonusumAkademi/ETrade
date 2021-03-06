﻿using System.Collections.Generic;
using Entities;

namespace Business.Abstract
{
    public interface IProductService:IValidator<Product>
    {
        Product GetById(int id);
        List<Product> GetAll();
        Product GetProductDetails(int id);
        List<Product> GetPopular();
        List<Product> GetProductsByCategory(string category, int page, int pagesize);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int GetCountByCategory(string category);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}