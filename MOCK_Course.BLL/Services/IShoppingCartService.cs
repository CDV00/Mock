using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface IShoppingCartService
    {
        Task<Responses<CartResponse>> GetAll(Guid userId);
        Task<Response<BaseResponse>> Add(CartRequest cartRequest);
        Task<BaseResponse> Remove(Guid userId);
    }
}
