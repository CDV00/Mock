using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;

namespace Repository.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IOrderItemQuery BuildQuery();

    }
}