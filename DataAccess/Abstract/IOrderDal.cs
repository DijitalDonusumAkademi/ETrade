using System.Collections.Generic;
using Entities;

namespace DataAccess.Abstract
{
    public interface IOrderDal:IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}