using Course.BLL.Requests;
using Course.BLL.Responses;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        public Task<Response<CartResponse>> Add(CartRequest cartRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Responses<CartResponse>> GetAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> Remove(Guid IdShoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
