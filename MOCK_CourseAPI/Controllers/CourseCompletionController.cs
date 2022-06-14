using Course.BLL.DTO;
using CourseAPI.Extensions.ControllerBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Course.BLL.Services.Abstraction;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseCompletionController : ControllerBase
    {
        private readonly ICourseCompletionService _courseCompletionService;
        public CourseCompletionController(ICourseCompletionService courseCompletionService)
        {
            _courseCompletionService = courseCompletionService;
        }

        /// <summary>
        /// check course completion
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("IsCompleted")]
        public async Task<ActionResult<BaseResponse>> IsCompleted(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _courseCompletionService.IsCompletion(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// User finished all lesson of course
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult<BaseResponse>> Create([FromBody] Guid courseId)
        //{
        //    var userId = User.GetUserId();
        //    var result = await _courseCompletionService.Add(userId, courseId);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
