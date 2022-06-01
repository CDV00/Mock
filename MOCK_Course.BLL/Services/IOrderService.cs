using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IOrderService
    {
        //Task<Responses<OrderResponse>> GetAll();
        Task<Responses<OrderDTO>> GetAll(Guid UserId);
        Task<Response<OrderDTO>> Add(Guid userId, OrderRequest orderRequest);
        //Task<Response<OrderResponse>> Update(OrderUpdateRequest orderUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
    }
}
