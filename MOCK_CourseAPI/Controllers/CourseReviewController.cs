using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        /// Get all review of course
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        [HttpGet("{CourseId:guid}")]
        public async Task<ActionResult<Responses<CourseReviewDTO>>> GetAll([FromQuery] Guid CourseId)
        {
            var result = await _courseReviewService.GetAll(CourseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Add new review
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
        /// update review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseReviewUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
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
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _courseReviewService.Delete(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Get Review of Course of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[HttpGet("Get-total/{userId}")]
        //public async Task<ActionResult<Response<int>>> GetTotal(Guid userId)
        //{
        //    var result = await _courseReviewService.GetTotal(userId);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
