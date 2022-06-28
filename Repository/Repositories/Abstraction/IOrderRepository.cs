using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IOrderRepository : IRepository<Order>
    {
        IOrderQuery BuildQuery();
        Task<OrderDTO> GetDetailOrder(Guid id);
        Task<PagedList<EarningDTO>> GetEarningAsync(OrderParameters orderParameters, Guid userId);
        Task<PagedList<StatementsDTO>> GetStatementsAsync(OrderParameters orderParameters, Guid userId);
    }
}
