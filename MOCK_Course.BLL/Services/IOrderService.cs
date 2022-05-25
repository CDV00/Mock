using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IOrderService
    {
        //Task<Responses<OrderResponse>> GetAll();
        Task<Responses<OrderResponse>> GetAll(Guid UserId);
        Task<Response<OrderResponse>> Add(OrderRequest orderRequest);
        //Task<Response<OrderResponse>> Update(OrderUpdateRequest orderUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
    }
}
