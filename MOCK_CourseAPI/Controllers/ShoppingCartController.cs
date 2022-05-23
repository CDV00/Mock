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
        public async Task<ActionResult<Responses<CartResponse>>> GetAll(Guid userId)
        {
            var result = await _shoppingCartService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Add Shopping Cart of user
        /// </summary>
        /// <param name="cartRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<CartResponse>>> Create([FromForm]CartRequest cartRequest)
        {
           
            var result = await _shoppingCartService.Add(cartRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Remove Shopping Cart
        /// </summary>
        /// <param name="IdShoppingCart"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Remove([FromForm]Guid IdShoppingCart)
        {
            var result = await _shoppingCartService.Remove(IdShoppingCart);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        //[HttpGet("total-cart")]
        //public async Task<ActionResult<Responses<int>>> TotalCart(Guid userId)
        //{
           
        //}
    }
}
