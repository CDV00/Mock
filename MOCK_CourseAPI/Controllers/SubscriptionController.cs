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
        /// Get Total subscriber of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Get-total-subscriber")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalSubscriber(Guid userId)
        {
            var result = await _subscriptionService.GetTotalSubscriber(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get the Total of the instructors that the student has registered
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-total-subscription")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalSubscription()
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.GetTotalSubscription(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
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

        /// <summary>
        /// Get all Subscriber
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-All-Subscriber")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> GetAllSubscriber(Guid userId)
        {
            var result = await _subscriptionService.GetAllSubscriber(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        // todo: get all subscription
        [HttpGet("Get-All-Subscription")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> GetAllSubscription()
        {
            var userId = User.GetUserId();

            var result = await _subscriptionService.GetAllSubscriber(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
