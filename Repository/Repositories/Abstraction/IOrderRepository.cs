using Course.BLL.Responses;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IOrderRepository : IRepository<Order>
    {
        IOrderQuery BuildQuery();
        Task<OrderDTO> GetDetailOrder(Guid id);
    }
}
