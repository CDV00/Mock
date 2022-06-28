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
using Entities.ParameterRequest;
using Course.BLL.Share.RequestFeatures;
using Entities.Extension;
using Entities.Constants;
using System.Text.Json;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
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

        /// <summary>
        /// Add New Order
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiOkResponse<OrderDTO>>> Add([FromBody] OrderRequest orderRequest)
        {
            var userId = User.GetUserId();
            var result = await _orderService.Add(userId, orderRequest);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }
        /// <summary>
        /// Is Purchased User Order
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get Detail Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponse<OrderDTO>>> GetDetailOrder(Guid id)
        {
            var result = await _orderService.GetDetail(id);
            if (!result.IsSuccess)
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
        [HttpGet("Get-all-Earning")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponses<EarningDTO>>> GetEarning([FromQuery] OrderParameters orderParameters)
        {
            Guid userId = User.GetUserId();

            var result = await _orderService.GetEarning(orderParameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<EarningDTO>>();

            Response.Headers.Add(SystemConstant.PagedHeader,
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(result);
        }
        [HttpGet("Get-all-Statements")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponses<StatementsDTO>>> GetStatements([FromQuery] OrderParameters orderParameters)
        {
            Guid userId = User.GetUserId();

            var result = await _orderService.GetStatements(orderParameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var statements = result.GetResult<PagedList<StatementsDTO>>();

            Response.Headers.Add(SystemConstant.PagedHeader,
                                 JsonSerializer.Serialize(statements.MetaData));

            return Ok(result);
        }
    }
}
