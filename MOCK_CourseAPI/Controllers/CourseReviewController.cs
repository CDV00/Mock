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

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseReviewController : ControllerBase
    {
        private readonly ICourseReviewService _courseReviewService;
        public CourseReviewController(ICourseReviewService courseReviewService)
        {
            _courseReviewService = courseReviewService;
        }

        /// <summary>
        /// Get all review of course. with paging and search
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="CourseId">Course Id</param>
        /// <returns></returns>
        [HttpGet("Get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CourseReviewDTO>>> GetAllCourses([FromQuery] Guid courseId, [FromQuery] CourseReviewParameters courseReviewParameters)
        {
            var result = await _courseReviewService.GetAll(courseId, courseReviewParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<CourseReviewDTO>(true, result));
        }


        /// <summary>
        /// Create new review
        /// </summary>
        /// <param name="courseReviewRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Responses<CourseReviewDTO>>> Add(CourseReviewRequest courseReviewRequest)
        {
            var result = await _courseReviewService.Add(courseReviewRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseReviewUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<ActionResult<BaseResponse>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest)
        {
            var result = await _courseReviewService.Update(id, courseReviewUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _courseReviewService.Delete(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Here:http://res.cloudinary.com/dnusjl4qg/image/upload/v1655028774/utxnqqfxqh6fc4galiu8.jpg
        /// if userId from query == null, It will get userId from token.
        /// </summary>
        [HttpGet("Get-total-Review-of-User")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalReviewOfUser(Guid? userId)
        {
            if (userId == null)
                userId = User.GetUserId();
            if (userId == null)
                return new Response<int>(false, "userId is null! need pass userId from Query or authentication", null);

            var result = await _courseReviewService.GetTotalReviewOfUser(userId.GetValueOrDefault());
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Here: http://res.cloudinary.com/dnusjl4qg/image/upload/v1655028638/jjlwudpo9jdudr78a0hd.jpg
        /// if userId from query == null, It will get userId from token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Get-total-Review-of-Instructor")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalReviewOfInstructor(Guid? userId)
        {
            if (userId == null)
                userId = User.GetUserId();
            if (userId == null)
                return new Response<int>(false, "userId is null! need pass userId from Query or authentication", null);

            var result = await _courseReviewService.GetTotalReviewOfInstructor(userId.GetValueOrDefault());
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get total review of course
        /// </summary>
        [HttpGet("Get-total-Review-of-course")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalReviewOfCourse(Guid courseId)
        {
            var result = await _courseReviewService.GetTotalReviewOfCourse(courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        /// <summary>
        /// Can only pass userId or courseId!!!
        /// Get Rating Percent from 1-5. if [0] = 1s, [1] = 2s,...
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>List float</returns>
        [HttpGet("Get-Rating")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<List<float>>>> GetRating(Guid? courseId, Guid? userId)
        {
            var result = await _courseReviewService.GetDetaiRate(courseId, userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }


        /// <summary>
        /// Can only pass userId or courseId!!!
        /// Get avg rating of course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>rating</returns>
        [HttpGet("Get-Avg-Rating")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<List<float>>>> GetAvgRating(Guid? courseId, Guid? userId)
        {
            var result = await _courseReviewService.GetAVGRatinng(courseId, userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Check User Course Review only review once
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("check-user-course-review")]
        public async Task<ActionResult<BaseResponse>> CheckUserCourseReview(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _courseReviewService.CheckUserCourseReview(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
