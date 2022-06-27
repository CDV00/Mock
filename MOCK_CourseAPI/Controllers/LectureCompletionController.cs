using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LectureCompletionController : ControllerBase
    {
        private readonly ILectureCompletionService _lessonCompletionService;
        public LectureCompletionController(ILectureCompletionService lessonCompletionService)
        {
            _lessonCompletionService = lessonCompletionService;
        }


        /// <summary>
        /// this api call, when one lesson completion
        /// </summary>
        /// <param name="lessonCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody] LectureCompletionRequest lessonCompletionRequest)
        {
            var userId = User.GetUserId();
            var result = await _lessonCompletionService.Add(userId, lessonCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update Time lecture completion
        /// </summary>
        /// <param name="lessonCompletionRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> Update([FromBody] LectureCompletionRequest lessonCompletionRequest)
        {
            var userId = User.GetUserId();
            var result = await _lessonCompletionService.Update(userId, lessonCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// check completion lecture
        /// </summary>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        [HttpGet("Is-Completion")]
        public async Task<ActionResult<BaseResponse>> IsCompletion(Guid lectureId)
        {
            var userId = User.GetUserId();
            var result = await _lessonCompletionService.IsCompletion(userId, lectureId);
            if (result.IsSuccess == false && result.StatusCode != null)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
