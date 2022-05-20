using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        /// <summary>
        /// Get all shoppingCart of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<Responses<CartResponse>> GetAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<Response<CartResponse>> Create(CartRequest cartRequest)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<BaseResponse> Remove(Guid IdShoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
