using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.BLL.Services;
using CourseAPI.Extensions.ControllerBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        ///  Add subscription
        /// </summary>
        /// <param name="courseCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody] SubscriptionRequest subscriptionRequest)
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.Add(userId, subscriptionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Get Total Course of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Get-total/{userId}")]
        public async Task<ActionResult<Response<int>>> GetTotal(Guid userId)
        {
            var result = await _subscriptionService.GetTotal(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
