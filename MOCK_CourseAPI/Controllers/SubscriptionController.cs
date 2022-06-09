using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.Services.Abstraction;
using System;
using System.Threading.Tasks;
using Course.BLL.Responses;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.DTO;

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
        /// <param name = "courseCompletionRequest" ></ param >
        /// < returns ></ returns >
        [HttpPost]
        public async Task<ActionResult<SubscriptionDTO>> Create(Guid SubscriberId)
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.Add(userId, SubscriberId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        /// <summary>
        /// Get Total Course of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Get-total")]
        public async Task<ActionResult<Response<int>>> GetTotal()
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.GetTotal(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("Get-total-Instrutors-Subscribing")]
        public async Task<ActionResult<Response<int>>> GetTotalInstrutorsSubscribing()
        {
            var userId = User.GetUserId();
            //var result = await _subscriptionService.GetTotalInstrutorsSubscribing(userId);
            //if (result.IsSuccess == false)
            //    return BadRequest(result);
            //return Ok(result);
            return Ok();
        }


        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete(Guid SubscriberId)
        {
            var userId = User.GetUserId();

            var result = await _subscriptionService.Delete(userId, SubscriberId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("Is-Subscription")]
        public async Task<ActionResult<SubscriptionDTO>> IsSubscription(Guid SubscriberId)
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.IsSubscription(userId, SubscriberId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("GetAllSubscripber")]
        public async Task<ActionResult<UserDTO>> GetUserSubscription()
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.GetUserSubscription(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        
    }
}
