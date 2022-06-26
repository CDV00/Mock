using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Query.Abstraction;
using Repository.Repositories.Abstraction;
using System;

namespace Repository.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private AppDbContext _context;
        public OrderItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderItemQuery BuildQuery()
        {
            return new OrderItemQuery(_context.OrderItems.AsQueryable(), _context);
        }
    }
}
