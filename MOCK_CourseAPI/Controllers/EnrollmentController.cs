using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;

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
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<EnrollmentDTO>>> Create(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _enrollmentService.Add(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("Get-total-enroll-of-user")]
        public async Task<ActionResult<Response<int>>> GetTotal()
        {
            var userId = User.GetUserId();
            var result = await _enrollmentService.GetTotal(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("check-enrollment")]
        public async Task<ActionResult<Response<EnrollmentDTO>>> IsEnrollment(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _enrollmentService.IsEnrollment(userId,courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
