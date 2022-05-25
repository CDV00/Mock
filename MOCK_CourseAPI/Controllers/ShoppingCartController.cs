using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
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
        /// https://gambolthemes.net/html-items/cursus_main_demo/shopping_cart.html
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
        /// when user click "add cart", will create new cart
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="cartRequest"></param>
        /// <returns></returns>
        [HttpPost("{UserId:guid}")]
        public async Task<ActionResult<Responses<CartResponse>>> Create(Guid UserId,[FromBody]CartRequest cartRequest)
        {
            var result = await _shoppingCartService.Add(UserId, cartRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Remove Shopping Cart
        /// https://gambolthemes.net/html-items/cursus_main_demo/shopping_cart.html
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{UserId:guid}")]
        public async Task<ActionResult<BaseResponse>> Remove([FromBody]Guid Id)
        {
            var result = await _shoppingCartService.Remove(Id);
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
