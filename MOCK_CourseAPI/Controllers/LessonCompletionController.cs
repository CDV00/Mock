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
    public class LessonCompletionController : ControllerBase
    {
        private readonly ILessonCompletionService _lessonCompletionService;
        public LessonCompletionController(ILessonCompletionService lessonCompletionService)
        {
            _lessonCompletionService = lessonCompletionService;
        }


        /// <summary>
        /// this api call, when one lesson completion
        /// </summary>
        /// <param name="lessonCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromQuery]LessonCompletionRequest lessonCompletionRequest)
        {
            var result = await _lessonCompletionService.Add(lessonCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
