using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
using Course.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseReviewController : ControllerBase
    {
        private readonly ICourseReviewService _courseReviewService;
        public CourseReviewController(ICourseReviewService courseReviewService)
        {
            _courseReviewService = courseReviewService;
        }
        [HttpGet]
        public async Task<ActionResult<Responses<CourseReviewResponse>>> GetAll()
        {
            var result = await _courseReviewService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Responses<CourseReviewResponse>>> GetById(Guid id)
        {
            var result = await _courseReviewService.GetById(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Responses<CourseReviewResponse>>> Add(CourseReviewRequest courseReviewRequest)
        {
            var result = await _courseReviewService.Add(courseReviewRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> Update(CourseReviewUpdateRequest courseReviewUpdateRequest)
        {
            var result = await _courseReviewService.Update(courseReviewUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _courseReviewService.Delete(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
