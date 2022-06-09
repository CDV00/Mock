using Course.BLL.Requests;
using Course.BLL.Responses;
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
    public class OrderController : ControllerBase
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
        public async Task<ActionResult<Response<int>>> GetTotal(Guid courseId)
        {
            var result = await _orderService.GetTotal(courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get all purchased courses user
        /// https://gambolthemes.net/html-items/cursus_main_demo/student_courses.html
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<Responses<OrderDTO>>> GetAll()
        {
            var userId = User.GetUserId();
            var result = await _orderService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Create new order record
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<OrderDTO>>> Add([FromBody] OrderRequest orderRequest)
        {
            var userId = User.GetUserId();
            var result = await _orderService.Add(userId, orderRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete order 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete([FromQuery] Guid id)
        {
            var result = await _orderService.Delete(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
