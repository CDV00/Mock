using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
