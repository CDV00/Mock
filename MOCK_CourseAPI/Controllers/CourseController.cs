using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Course.BLL.Responses;

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
        /// Get all course
        /// https://gambolthemes.net/html-items/cursus_main_demo/explore.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-all")]
        public async Task<ActionResult<Responses<CoursesCardResponse>>> GetAll()
        {
            var result = await _coursesService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        /// <summary>
        /// Get Detail course by course id
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}", Name = "Get")]
        public async Task<ActionResult<Response<CourseResponse>>> Get(Guid id)
        {
            var course = await _coursesService.Get(id);
            if (course.IsSuccess == false)
                return BadRequest(course);
            return Ok(course);
        }

        /// <summary>
        /// Create new course | 
        /// https://gambolthemes.net/html-items/cursus_main_demo/create_new_course.html | 
        /// UserId:9e47da69-3d3e-428d-a395-d53908753582
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CourseResponse>> Create([FromBody] CourseRequest courseRequest)
        {
            var result = await _coursesService.Add(courseRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// update course by id
        /// don't have Page UI yet!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CoursesUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Response<CourseResponse>>> Update(Guid id, UpdateCourseRequest CoursesUpdateRequest)
        {
            var result = await _coursesService.Update(id, CoursesUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete an Courses by id
        /// https://gambolthemes.net/html-items/cursus_main_demo/instructor_courses.html#
        /// </summary>
        /// <param name="id">Id Courses</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _coursesService.Remove(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
