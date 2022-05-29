using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.BLL.Services;
using CourseAPI.Extensions.ControllerBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        /// User finished all lesson of course
        /// </summary>
        /// <param name="courseCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody] CourseCompletionRequest courseCompletionRequest)
        {
            var userId = User.GetUserId();
            var result = await _courseCompletionService.Add(userId, courseCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
