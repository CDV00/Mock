using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        /// <summary>
        /// Get all Enrollment of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ActionResult<Responses<EnrollmentResponse>>> GetAll(Guid userId)
        {
            var result = await _enrollmentService.GetAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Add Enrollment of user
        /// </summary>
        /// <param name="enrollmentRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Create([FromBody]EnrollmentRequest enrollmentRequest)
        {
            var result = await _enrollmentService.Add(enrollmentRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Update Enrollment
        /// </summary>
        /// <param name="enrollmentUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<EnrollmentResponse>> Update(EnrollmentUpdateRequest enrollmentUpdateRequest)
        {
            var result = await _enrollmentService.Update(enrollmentUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Remove Enrollment
        /// </summary>
        /// <param name="errollmentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Remove([FromForm] Guid errollmentId)
        {
            var result = await _enrollmentService.Remove(errollmentId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
