using Course.DAL.Models;
using Course.DAL.Queries;
using System;

namespace Course.DAL.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        IOrderQuery BuildQuery();
    }
}
