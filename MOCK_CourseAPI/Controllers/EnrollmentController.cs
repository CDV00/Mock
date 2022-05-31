using Course.BLL.Requests;
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
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        /// <summary>
        /// when user press enrellment one course
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="enrollmentRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody] EnrollmentRequest enrollmentRequest)
        {
            var userId = User.GetUserId();
            var result = await _enrollmentService.Add(userId, enrollmentRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Get Errollment of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[HttpGet("Get-total/{userId}")]
        //public async Task<ActionResult<Response<int>>> GetTotal(Guid userId)
        //{
        //    var result = await _enrollmentService.GetTotal(userId);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
