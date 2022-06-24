using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using CourseAPI.Presentation.Controllers;
using Entities.Responses;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get total course has been sold
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("Get-Total")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponse<int>>> GetTotal(Guid courseId)
        {
            var result = await _orderService.GetTotal(courseId);
            if (result.IsSuccess == false)
                return ProcessError(result);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<ApiOkResponse<OrderDTO>>> Add([FromBody] OrderRequest orderRequest)
        {
            var userId = User.GetUserId();
            var result = await _orderService.Add(userId, orderRequest);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }

        [HttpGet("Is-Purchased")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiBaseResponse>> IsPurchased(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _orderService.IsPurchased(courseId, userId);
            if (result.IsSuccess == false)
                return ProcessError(result);
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<ActionResult<>>
        // Get total purchase of user

        /// <summary>
        /// Get all purchased courses user
        /// https://gambolthemes.net/html-items/cursus_main_demo/student_courses.html
        /// </summary>
        /// <returns></returns>
        //[HttpGet()]
        //public async Task<ActionResult<Responses<OrderDTO>>> GetAll()
        //{
        //    var userId = User.GetUserId();
        //    var result = await _orderService.GetAll(userId);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Create new order record
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
    }
}
