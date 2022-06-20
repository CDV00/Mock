using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.Services.Abstraction;
using System;
using System.Threading.Tasks;
using Course.BLL.Responses;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.DTO;
using System.Text.Json;
using Entities.ParameterRequest;

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
        ///  Subscription
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Response<SubscriptionDTO>>> Create(Guid instructorId)
        {
            var userId = User.GetUserId();
            if (userId == instructorId)
                return new Response<SubscriptionDTO>(false, "can't register yourself", null);

            var result = await _subscriptionService.Add(userId, instructorId);
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
            var result = await _subscriptionService.GetTotalInstructor(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// unscription
        /// </summary>
        /// <param name="SubscriberId"></param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete(Guid SubscriberId)
        {
            var userId = User.GetUserId();

            var result = await _subscriptionService.Delete(userId, SubscriberId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Check Is subscription
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        [HttpGet("Is-Subscription")]
        public async Task<ActionResult<SubscriptionDTO>> IsSubscription(Guid instructorId)
        {
            var userId = User.GetUserId();
            var result = await _subscriptionService.IsSubscription(userId, instructorId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get all Subscriber, user has been registed
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-All-Subscriber")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<UserDTO>>> GetAllSubscriber([FromQuery] Guid userId,
            [FromQuery] SubscriptionParameters subscriptionParameters)
        {
            var result = await _subscriptionService.GetAllSubscriber(userId, subscriptionParameters);

            Response.Headers.Add("X-Pagination",
                                JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<UserDTO>(true, result));
        }


        /// <summary>
        /// Get all instructor, user has registed
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-All-Subscription")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<UserDTO>>> GetAllInstructor([FromQuery] SubscriptionParameters subscriptionParameters)
        {
            var userId = User.GetUserId();

            var result = await _subscriptionService.GetAllInstructor(subscriptionParameters, userId);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<UserDTO>(true, result));
        }
    }
}
