using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Query.Abstraction;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories.Abstraction
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IOrderItemQuery BuildQuery();
        Task<PagedList<EarningDTO>> GetEarningAsync(OrderParameters orderParameters, Guid userId);
        Task<PagedList<OrderItemDTO>> GetStatements(DepositParameters depositParameters, Guid userid);
    }
}