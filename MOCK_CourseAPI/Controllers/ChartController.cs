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
    public class ChartController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        public ChartController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }


        /// <summary>
        /// Create new order record
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpGet("Sale-Of-Year")]
        public async Task<ActionResult<ApiOkResponses<EarningDTO>>> SaleOfYear([FromQuery] OrderParameters orderParameters)
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
         }
}
