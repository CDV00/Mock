﻿using System.Threading.Tasks;
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

        [HttpGet("Get-all-course")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAllCourses([FromQuery] CourseParameters courseParameters)
        {
            var Pageresult = await _coursesService.GetCoursesAsync(courseParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(Pageresult.metaData));

            return Ok(Pageresult.courses);
        }

        /// <summary>
        /// Get all course for Page:
        /// https://gambolthemes.net/html-items/cursus_main_demo/explore.html
        /// </summary>
        /// <returns></returns>
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
        /// Get Detail of course to render page: https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("Get-Detail-Course")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CourseForDetailDTO>>> GetDetail(Guid id)
        //{
        //    var result = await _coursesService.GetDetail(id);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Get Detail course For Update Course
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <returns></returns>
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
        /// Create new course | 
        /// https://gambolthemes.net/html-items/cursus_main_demo/create_new_course.html | 
        /// UserId:9e47da69-3d3e-428d-a395-d53908753582
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
        /// update course by id
        /// don't have Page UI yet!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CoursesUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Response<CourseDTO>>> Update(Guid id, CourseForUpdateRequest CoursesUpdateRequest)
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
        /// <summary>
        /// Get Total Course of User
        /// </summary>
        [HttpGet("Get-total-courses")]
        public async Task<ActionResult<Response<int>>> GetTotal()
        {
            var userId = User.GetUserId();
            var result = await _coursesService.GetTotal(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get all My Course
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-all-my-course")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAllMyCoures()
        {
            var userId = User.GetUserId();
            var result = await _coursesService.GetAllMyCoures(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        /// <summary>
        /// Get all Upcomming Course
        /// 
        /// </summary>
        ///<param name="userId"> User Id Courses</param>
        /// <returns></returns>
        //[HttpGet("Get-all-upcoming-courses")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<UpcommingCourseDTO>>> GetAllUpcomingCourses()
        //{
        //    var userId = User.GetUserId();
        //    var result = await _coursesService.GetAllUpcomingCourses(userId);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Get all My Purchase
        /// 
        /// </summary>
        ///<param name="userId"> User Id Courses</param>
        /// <returns></returns>
        [HttpGet("Get-all-my-purchse")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseDTO>>> GetAllMyPurchase()
        {
            var userId = User.GetUserId();
            var result = await _coursesService.GetAllMyPurchase(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("is-courses-free/{courseId}")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseDTO>>> IsCoursesFree(Guid courseId)
        {
            var result = await _coursesService.IsFree(courseId);
            if (result.IsSuccess == false && result.Message != null)
                return BadRequest(result);
            return Ok(result);
        }
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
