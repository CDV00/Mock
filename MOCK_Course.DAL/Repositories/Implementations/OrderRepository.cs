using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class OrderRepository: Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {

        }
    }
}
