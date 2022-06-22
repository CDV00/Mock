using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IOrderItemService
    {
        Task<Response<OrderItemDTO>> IsPurchased(Guid userId, Guid courseId);
    }
}
