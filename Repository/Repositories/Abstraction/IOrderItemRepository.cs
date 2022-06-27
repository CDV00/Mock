using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Query.Abstraction;

namespace Repository.Repositories.Abstraction
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IOrderItemQuery BuildQuery();

    }
}