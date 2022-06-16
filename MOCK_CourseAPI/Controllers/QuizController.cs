using Course.BLL.Requests;
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

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
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
        public async Task<ActionResult<Responses<QuizDTO>>> GetAll([FromQuery] QuizParameters quizParameters)
        {
            var result = await _quizService.GetAll(quizParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<QuizDTO>(true, result));
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
