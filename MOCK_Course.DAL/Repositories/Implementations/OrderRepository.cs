using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        private AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderQuery BuildQuery()
        {
            return new OrderQuery(_context.Orders.AsQueryable(), _context);
        }
    }
}
