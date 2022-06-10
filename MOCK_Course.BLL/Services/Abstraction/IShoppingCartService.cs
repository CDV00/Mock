using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services.Abstraction
{
    public interface IShoppingCartService
    {
        Task<Responses<CartDTO>> GetAll(Guid userId);
        Task<BaseResponse> Remove(Guid userId);
        Task<Response<CartDTO>> Add(Guid userId, Guid courseId);
        Task<Response<CartUpdateDTO>> Update(Guid id, CartUpdateRequest cartUpdateRequest);
    }
}
