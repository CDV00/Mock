using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<CartDTO>>> GetAll()
        {
            var userId = User.GetUserId();
            var result = await _shoppingCartService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Create new cart
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Responses<CartDTO>>> Create(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _shoppingCartService.Add(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Update IsActive Shopping Cart 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Response<CartDTO>>> Update(Guid id, CartUpdateRequest cartUpdateRequest)
        {
            var result = await _shoppingCartService.Update(id, cartUpdateRequest);
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
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Remove(Guid Id)
        {
            Guid userId = User.GetUserId();
            var result = await _shoppingCartService.Remove(Id, userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
