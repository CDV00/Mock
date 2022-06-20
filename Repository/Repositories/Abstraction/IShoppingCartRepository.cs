using Course.BLL.DTO;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        IShoppingCartQuery BuildQuery();
        Task<bool> CheckPriceGreaterThanZero(Guid courseId);
        Task<PagedList<CartDTO>> GetAllAsync(CartParameters parameters, Guid userId);
    }
}
