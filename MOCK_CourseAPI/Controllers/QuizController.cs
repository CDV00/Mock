using Course.BLL.Responses;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Course.BLL.Services.Abstraction;
using CourseAPI.Extensions.ControllerBase;
using System.Collections.Generic;
using System.Text.Json;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Entities.ParameterRequest;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ApiControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        /// <summary>
        /// Get all Quiz of course. with paging and search
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="sectionId">Course Id</param>
        /// <returns></returns>
        [HttpGet("Get-all-quiz")]
        [AllowAnonymous]
        
        public async Task<ActionResult<ApiOkResponses<QuizDTO>>> GetAll([FromQuery] QuizParameters parameters)
        {
            var result = await _quizService.GetAllQuiz(parameters);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<QuizDTO>>();

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(new Responses<QuizDTO>(true, coursePagedList));
        }
        //[HttpPost]
        //public async Task<ActionResult<Response<QuizDTO>>> Add(QuizForCreateRequest QuizForCreateRequest)
        //{
        //    var result = await _QuizService.Add(QuizForCreateRequest);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
