using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _coursesService;
        public CourseController(ICourseService coursesService)
        {
            _coursesService = coursesService;
        }
        /// <summary>
        /// Error mapper Courses and user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<CoursesCartResponse>>> GetAll()
        {
            var result = await _coursesService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Add([FromBody] CourseRequest courseRequest)
        {
            var result = await _coursesService.Add(courseRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Response<CourseResponse>>> Update(UpdateCourseRequest CoursesUpdateRequest)
        {
            var result = await _coursesService.Add(CoursesUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete an Courses
        /// </summary>
        /// <param name="Id">Id Courses</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Delete(Guid Id)
        {
            var result = await _coursesService.Remove(Id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
