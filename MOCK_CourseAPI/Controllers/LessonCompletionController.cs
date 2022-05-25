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
    public class LessonCompletionController : ControllerBase
    {
        private readonly ILessonCompletionService _lessonCompletionService;
        public LessonCompletionController(ILessonCompletionService lessonCompletionService)
        {
            _lessonCompletionService = lessonCompletionService;
        }


        /// <summary>
        /// Get All Lesson Completion
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<LessonCompletionResponse>>> GetAll(Guid userId)
        {
            var result = await _lessonCompletionService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        
        /// <summary>
        /// Add Lesson Completion
        /// </summary>
        /// <param name="courseCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody]LessonCompletionRequest lessonCompletionRequest)
        {
            var result = await _lessonCompletionService.Add(lessonCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update Lesson Completion
        /// </summary>
        /// <param name="lessonCompletionUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<LessonCompletionResponse>> Update(LessonCompletionUpdateRequest lessonCompletionUpdateRequest)
        {
            var result = await _lessonCompletionService.Update(lessonCompletionUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        ///  Remove Lesson Completion
        /// </summary>
        /// <param name="IdlessonCompletion"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Remove([FromForm] Guid IdlessonCompletion)
        {
            var result = await _lessonCompletionService.Remove(IdlessonCompletion);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
