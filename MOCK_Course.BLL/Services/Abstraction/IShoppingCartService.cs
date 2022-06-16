using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services.Abstraction
{
    public interface IShoppingCartService
    {
        Task<Responses<CartDTO>> GetAll(Guid userId);
        Task<BaseResponse> Remove(Guid courseId, Guid userId);
        Task<Response<CartDTO>> Add(Guid userId, Guid courseId);
        Task<Response<CartDTO>> Update(Guid userId, CartUpdateRequest cartUpdateRequest);
    }
}
