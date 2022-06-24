using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IOrderService
    {
        Task<ApiBaseResponse> Add(Guid userId, OrderRequest orderRequest);
        Task<ApiBaseResponse> GetDetail(Guid id);
        Task<ApiBaseResponse> GetTotal(Guid courseId);
        Task<ApiBaseResponse> IsPurchased(Guid userId, Guid courseId);
    }
}
