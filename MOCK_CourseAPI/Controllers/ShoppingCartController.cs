using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using CourseAPI.Presentation.Controllers;
using Entities.Responses;
using Entities.ParameterRequest;
using Entities.Extension;
using Course.BLL.Share.RequestFeatures;
using Entities.Constants;
using System.Text.Json;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ApiControllerBase
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
        public async Task<ActionResult<ApiOkResponses<CartDTO>>> GetAll([FromQuery] CartParameters parameters)
        {
            var userId = User.GetUserId();
            var result = await _shoppingCartService.GetAllAsync(parameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var carts = result.GetResult<PagedList<CartDTO>>();
            Response.Headers.Add(SystemConstant.PagedHeader,
                               JsonSerializer.Serialize(carts.MetaData));

            return Ok(result);
        }

        /// <summary>
        /// Create new cart
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiOkResponse<CartDTO>>> Create(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _shoppingCartService.Add(userId, courseId);

            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Update IsActive Shopping Cart 
        /// </summary>
        /// <param name="cartUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Response<CartDTO>>> Update(CartUpdateRequest cartUpdateRequest)
        {
            var userId = User.GetUserId();
            var result = await _shoppingCartService.Update(userId, cartUpdateRequest);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Remove Shopping Cart
        /// https://gambolthemes.net/html-items/cursus_main_demo/shopping_cart.html
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<ApiBaseResponse>> Remove(Guid courseId)
        {
            Guid userId = User.GetUserId();
            var result = await _shoppingCartService.Remove(courseId, userId);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }
    }
}
