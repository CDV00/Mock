﻿//using Course.BLL.Requests;
//using Course.BLL.DTO;
//using Course.BLL.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;

//namespace CourseAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Admin,Instructor")]
//    public class LessonController : ControllerBase
//    {
//        private readonly ILessonService _LessonService;
//        public LessonController(ILessonService LessonService)
//        {
//            _LessonService = LessonService;
//        }

//        /// <summary>
//        /// Get all lesson of course sections
//        /// https://gambolthemes.net/html-items/cursus_main_demo/create_new_course.html
//        /// </summary>
//        /// <param name="courseId"></param>
//        /// <returns></returns>
//        [HttpGet("{sectionId:guid}")]
//        public async Task<ActionResult<Responses<LessonDTO>>> GetAll([FromQuery] Guid sectionId)
//        {
//            var result = await _LessonService.GetAll(sectionId);
//            if (result.IsSuccess == false)
//                return BadRequest(result);
//            return Ok(result);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Response<LessonDTO>>> Create([FromBody] LessonForCreateRequest LessonRequest)
//        {
//            var result = await _LessonService.Add(LessonRequest);
//            if (result.IsSuccess == false)
//                return BadRequest(result);
//            return Ok(result);
//        }

//        /// <summary>
//        /// Update lesson by lesson Id
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="LessonUpdateRequest"></param>
//        /// <returns></returns>
//        [HttpPut("{id:guid}")]
//        public async Task<ActionResult<Response<LessonDTO>>> Update(Guid id, [FromBody] LessonForUpdateRequest LessonUpdateRequest)
//        {
//            var result = await _LessonService.Update(id, LessonUpdateRequest);
//            if (result.IsSuccess == false)
//                return BadRequest(result);
//            return Ok(result);
//        }

//        /// <summary>
//        /// Remove lesson by lesson id
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpDelete("{id:guid}")]
//        public async Task<ActionResult<BaseResponse>> Remove([FromQuery] Guid id)
//        {
//            var result = await _LessonService.Remove(id);
//            if (result.IsSuccess == false)
//                return BadRequest(result);
//            return Ok(result);
//        }
//    }
//}
