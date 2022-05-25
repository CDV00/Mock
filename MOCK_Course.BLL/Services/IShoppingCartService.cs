using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface IShoppingCartService
    {
        Task<Responses<CartResponse>> GetAll(Guid userId);
        Task<Response<CartResponse>> Add(Guid UserId,CartRequest cartRequest);
        Task<BaseResponse> Remove(Guid userId);
    }
}
