using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using Entities.Responses;
using Entities.ParameterRequest;
using Entities.DTOs;

namespace Course.BLL.Services.Abstraction
{
    public interface IOrderService
    {
        Task<ApiBaseResponse> Add(Guid userId, OrderRequest orderRequest);
        Task<ApiOkResponse<ListAnalysisOrderResponse>> SumMoneyOrderByMonth(Guid userId);
        Task<ApiBaseResponse> GetDetail(Guid id);
        Task<ApiBaseResponse> GetEarning(OrderParameters orderParameters, Guid userId);
        Task<ApiBaseResponse> GetStatements(OrderParameters orderParameters, Guid userId);
        Task<ApiBaseResponse> GetTotal(Guid courseId);
        Task<ApiBaseResponse> IsPurchased(Guid userId, Guid courseId);
        Task<ApiOkResponse<ListSaleAnalysisResponse>> CountOrderByWeek(Guid userId);
    }
}
