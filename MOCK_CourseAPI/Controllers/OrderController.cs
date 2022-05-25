using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<Responses<OrderResponse>>> GetAll()
        {
            var result = await _orderService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Responses<OrderResponse>>> GetById(Guid id)
        {
            var result = await _orderService.GetById(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Responses<OrderResponse>>> Add(OrderRequest orderRequest)
        {
            var result = await _orderService.Add(orderRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> Update(OrderUpdateRequest orderUpdateRequest)
        {
            var result = await _orderService.Update(orderUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _orderService.Delete(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
