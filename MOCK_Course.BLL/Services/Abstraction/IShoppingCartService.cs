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
        Task<ApiBaseResponse> GetAllAsync(CartParameters parameters, Guid userId);
        Task<ApiBaseResponse> Add(Guid userId, Guid courseId);
        Task<ApiBaseResponse> Update(Guid userId, CartUpdateRequest cartUpdate);
        Task<ApiBaseResponse> Remove(Guid courseId, Guid userId);
    }
}
