using Course.BLL.Requests;
using Course.BLL.Responses;
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
        /// Get All Course Completion
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<CourseCompletionResponse>>> GetAll(Guid userId)
        {
            var result = await _courseCompletionService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        
        /// <summary>
        /// Add Course Completion
        /// </summary>
        /// <param name="courseCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody]CourseCompletionRequest courseCompletionRequest)
        {
            var result = await _courseCompletionService.Add(courseCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update Course Completion
        /// </summary>
        /// <param name="courseCompletionUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<CourseCompletionResponse>> Update(CourseCompletionUpdateRequest courseCompletionUpdateRequest)
        {
            var result = await _courseCompletionService.Update(courseCompletionUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        ///  Remove Course Completion
        /// </summary>
        /// <param name="IdcourseCompletion"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Remove([FromForm]Guid IdcourseCompletion)
        {
            var result = await _courseCompletionService.Remove(IdcourseCompletion);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
