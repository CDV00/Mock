using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Entities.Responses;
using Entities.ParameterRequest;

namespace Course.BLL.Services.Abstraction
{
    public interface IShoppingCartService
    {
        Task<BaseResponse> Remove(Guid courseId, Guid userId);
        Task<Response<CartDTO>> Add(Guid userId, Guid courseId);
        Task<Response<CartDTO>> Update(Guid userId, CartUpdateRequest cartUpdateRequest);
        Task<ApiBaseResponse> GetAllAsync(CartParameters parameters, Guid userId);
    }
}
