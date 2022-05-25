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
        public async Task<ActionResult<BaseResponse>> Create([FromQuery]CourseCompletionRequest courseCompletionRequest)
        {
            var result = await _courseCompletionService.Add(courseCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
