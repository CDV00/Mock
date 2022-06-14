using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        IShoppingCartQuery BuildQuery();
        Task<bool> CheckPriceGreaterThanZero(Guid courseId);
    }
}
