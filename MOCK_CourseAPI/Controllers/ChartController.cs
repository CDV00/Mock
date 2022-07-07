using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using CourseAPI.Presentation.Controllers;
using Entities.Responses;
using Entities.DTOs;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChartController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IOrderItemService _orderItemService;
        public ChartController(IOrderService orderService, IOrderItemService orderItemService,
            ISubscriptionService subscriptionService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _subscriptionService = subscriptionService;
        }


        /// <summary>
        /// Create new order record
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpGet("sum-price-sale-of-course-group-by-moth")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponse<ListAnalysisOrderResponse>>> CountMoneyOrderByMonth()
        {
            Guid userId = User.GetUserId();

            var result = await _orderService.SumMoneyOrderByMonth(userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        [HttpGet("count-sale-of-course-by-week")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponse<ListSaleAnalysisResponse>>> CountOrderByWeek()
        {
            Guid userId = User.GetUserId();

            var result = await _orderService.CountOrderByWeek(userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        [HttpGet("count-subscription-by-month")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponse<ListSaleAnalysisResponse>>> CountSubcriberByMonth()
        {
            Guid userId = User.GetUserId();

            var result = await _subscriptionService.CountSubcriberByMonth(userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }
    }
}
