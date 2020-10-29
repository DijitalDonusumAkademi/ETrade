using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class OrderDal : Repository<Order,AppDbContext>,IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new AppDbContext())
            {
                var orders = context.Orders
                    .Include(i => i.OrderItems)
                    .ThenInclude(i => i.Product)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId);
                }

                return orders.ToList();
            }
        }
    }
}