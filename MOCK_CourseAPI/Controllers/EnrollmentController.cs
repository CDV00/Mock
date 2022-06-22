using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using Entities.Responses;
using CourseAPI.Presentation.Controllers;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController : ApiControllerBase
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
        public async Task<ActionResult<ApiOkResponse<EnrollmentDTO>>> Create(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _enrollmentService.Add(userId, courseId);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Get total enrollment of user. if userId from query == null, It will get userId from token.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-total-enroll-of-user")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalOfUser(Guid? userId)
        {
            if (userId == null)
                userId = User.GetUserId();
            if (userId == null)
                return new Response<int>(false, "userId is null! need pass userId from Query or authentication", null);

            var result = await _enrollmentService.GetTotalEnrollOfUser(userId.GetValueOrDefault());
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// if userId from query == null, It will get userId from token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Get-total-enroll-of-instructor")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalOfInstructor(Guid? userId)
        {
            if (userId == null)
                userId = User.GetUserId();
            if (userId == null)
                return new Response<int>(false, "userId is null! need pass userId from Query or authentication", null);

            var result = await _enrollmentService.GetTotalEnrollOfInstructor(userId.GetValueOrDefault());
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get total enrollment of an course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("Get-total-enroll-of-course")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotalEnrollOfCourse(Guid courseId)
        {
            var result = await _enrollmentService.GetTotalEnrollOfCourse(courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// check enrollment
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("check-enrollment")]
        public async Task<ActionResult<Response<EnrollmentDTO>>> IsEnrollment(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _enrollmentService.IsEnrollment(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("Get-all-enrollment-course")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<EnrollmentDTO>>> GetAll()
        {
            Guid? userId = (User.GetUserId() == Guid.Empty) ? null : User.GetUserId();
            var result = await _enrollmentService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
