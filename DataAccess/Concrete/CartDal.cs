using System;
using System.Linq;
using DataAccess.Abstract;
using Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataAccess.Concrete
{
    public class CartDal : Repository<Cart,AppDbContext>,ICartDal
    {
        public override void Update(Cart entity)
        {
            using (var context = new AppDbContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }

        public Cart GetByUserId(string userId)
        {
            using (var context = new AppDbContext())
            {
                return context.Carts.Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new AppDbContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, cartId, productId);
            }
        }

        public void ClearCart(object cartId)
        {
            using (var context = new AppDbContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlCommand(cmd, cartId);
            }
        }
    }
}