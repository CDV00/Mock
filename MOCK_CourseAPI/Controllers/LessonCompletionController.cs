﻿using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<BaseResponse>> Create([FromBody] LessonCompletionRequest lessonCompletionRequest)
        {
            var userId = User.GetUserId();
            var result = await _lessonCompletionService.Add(userId,lessonCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
