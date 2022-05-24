using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface IShoppingCartService
    {
        Task<Responses<CartResponse>> GetAll(Guid userId);
        Task<Response<Responsesnamespace.BaseResponse>> Add(CartRequest cartRequest);
        Task<Responsesnamespace.BaseResponse> Remove(Guid userId);
    }
}
