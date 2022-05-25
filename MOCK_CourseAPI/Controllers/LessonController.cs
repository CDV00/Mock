using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _LessonService;
        public LessonController(ILessonService LessonService)
        {
            _LessonService = LessonService;
        }

        /// <summary>
        /// Get all lesson of course sections
        /// https://gambolthemes.net/html-items/cursus_main_demo/create_new_course.html
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<LessonResponse>>> GetAll([FromBody]Guid courseId)
        {
            var result = await _LessonService.GetAll(courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("{SectionId:guid}")]
        public async Task<ActionResult<Response<LessonResponse>>> Create(Guid SectionId,[FromBody]LessonCreateRequest LessonRequest)
        {
            var result = await _LessonService.Add(SectionId, LessonRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update lesson by lesson Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="LessonUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("{UserId:guid}")]
        public async Task<ActionResult<Response<LessonResponse>>> Update(Guid id,[FromBody]LessonUpdateRequest LessonUpdateRequest)
        {
            var result = await _LessonService.Update(id, LessonUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Remove lesson by lesson id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{UserId:guid}")]
        public async Task<ActionResult<BaseResponse>> Remove(Guid id)
        {
            var result = await _LessonService.Remove(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
