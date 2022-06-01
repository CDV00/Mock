using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface IShoppingCartService
    {
        Task<Responses<CartDTO>> GetAll(Guid userId);
        Task<Response<CartDTO>> Add(Guid userId, CartRequest cartRequest);
        Task<BaseResponse> Remove(Guid userId);
    }
}
