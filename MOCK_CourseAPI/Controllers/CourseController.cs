using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using Course.BLL.Responses;
using Microsoft.AspNetCore.Authorization;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System.Text.Json;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Instructor")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _coursesService;
        public CourseController(ICourseService coursesService)
        {
            _coursesService = coursesService;
        }

        /// <summary>
        /// Get all course with paging and filter
        /// </summary>
        /// <param name="courseParameters"></param>
        /// <returns>List of course</returns>
        [HttpGet("Get-all-course")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAllCourses([FromQuery] CourseParameters courseParameters)
        {
            var result = await _coursesService.GetCoursesAsync(courseParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<CourseDTO>(true, result));
        }

        /// <summary>
        /// Get all course! This API is old, need use Get-all-course to paging and filter
        /// </summary>
        /// <returns>List of course</returns>
        [HttpGet("Get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAll()
        {
            var result = await _coursesService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get Detail course Include User, Category, Language, Levels 
        /// </summary>
        /// <returns>an course</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Response<CourseDTO>>> Get(Guid id)
        {
            var course = await _coursesService.Get(id);
            if (course.IsSuccess == false)
                return BadRequest(course);
            return Ok(course);
        }

        /// <summary>
        /// Create new course Include Sections, lecture 
        /// </summary>
        /// <param name="courseRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> Create([FromBody] CourseForCreateRequest courseRequest)
        {
            var userId = User.GetUserId();
            var result = await _coursesService.Add(userId, courseRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CoursesUpdateRequest"></param>
        /// <returns>an course</returns>
        [HttpPut()]
        public async Task<ActionResult<Response<CourseDTO>>> Update(Guid id, CourseForUpdateRequest CoursesUpdateRequest)
        {
            var result = await _coursesService.Update(id, CoursesUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete an Courses by Id
        /// </summary>
        /// <param name="id">Id Courses</param>
        /// <returns>true or false</returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _coursesService.Remove(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Get Total Course of Instructor 
        /// </summary>
        /// <returns>total course as Integer type</returns>
        [HttpGet("Get-total-courses")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotal(Guid UserId)
        {
            var result = await _coursesService.GetTotalCourseOfUser(UserId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get all My Course of Instructor
        /// </summary>
        /// <returns>List Courses</returns>
        [HttpGet("Get-all-my-course")]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAllMyCoures()
        {
            var userId = User.GetUserId();
            var result = await _coursesService.GetAllMyCoures(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        /// <summary>
        /// Get all Course, User already purchased
        /// </summary>
        /// <returns>List Courses</returns>
        [HttpGet("Get-all-my-purchased")]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAllMyPurchase()
        {
            var userId = User.GetUserId();
            var result = await _coursesService.GetAllMyPurchase(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }





























        //[HttpGet("is-courses-free/{courseId}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CourseDTO>>> IsCoursesFree(Guid courseId)
        //{
        //    var result = await _coursesService.IsFree(courseId);
        //    if (result.IsSuccess == false && result.Message != null)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
        /// <summary>
        /// Erroring
        /// Comming soon...
        /// </summary>
        /// <param name="cousrsePagingRequest"></param>
        /// <returns></returns>
        //[HttpGet("Get-course-paing")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CousrsePagingDTO>>> GetCoursePaings([FromQuery] CousrsePagingRequest cousrsePagingRequest)
        //{
        //    var result = await _coursesService.GetCoursePaing(cousrsePagingRequest);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Erroring
        /// Comming soon...
        /// </summary>
        /// <param name="cousrsePagingRequest"></param>
        /// <returns></returns>
        //[HttpPost("Search/{key}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CousrsePagingDTO>>> GetByFilteringCousrse(string key)
        //{
        //    var result = await _coursesService.GetByFilteringCousrse(key);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
