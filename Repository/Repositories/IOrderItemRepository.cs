using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IOrderItemQuery BuildQuery();
        Task<PagedList<OrderItemDTO>> GetStatements(DepositParameters depositParameters, Guid userid);
    }
}