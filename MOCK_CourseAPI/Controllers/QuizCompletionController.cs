using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System;
using Course.BLL.Services;
using Entities.DTOs;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizCompletionController : ControllerBase
    {
        private readonly IQuizCompletionService _quizCompletionService;
        public QuizCompletionController(IQuizCompletionService quizCompletionService)
        {
            _quizCompletionService = quizCompletionService;
        }


        /// <summary>
        /// Create new Quiz completion
        ///</summary>
        /// <param name="quizCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<QuizCompletionDTO>> Create([FromBody] QuizCompletionRequest quizCompletionRequest)
        {
            var userId = User.GetUserId();
            var result = await _quizCompletionService.Add(userId, quizCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        ///// <summary>
        ///// check completion lecture
        ///// </summary>
        ///// <param name="lectureId"></param>
        ///// <returns></returns>
        //[HttpGet("Is-Completion")]
        //public async Task<ActionResult<BaseResponse>> IsCompletion(Guid lectureId)
        //{
        //    var userId = User.GetUserId();
        //    var result = await _quizCompletionService.IsCompletion(userId, lectureId);
        //    if (result.IsSuccess == false && result.StatusCode != null)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
