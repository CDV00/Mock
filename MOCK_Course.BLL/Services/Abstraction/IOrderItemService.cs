using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using Entities.Responses;
using Entities.ParameterRequest;

namespace Course.BLL.Services.Abstraction
{
    public interface IOrderItemService
    {
        Task<ApiBaseResponse> GetStatements(DepositParameters orderParameters, Guid userId);
        Task<Response<OrderItemDTO>> IsPurchased(Guid userId, Guid courseId);
    }
}
