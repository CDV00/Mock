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
using Course.BLL.Share.RequestFeatures;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        /// <summary>
        /// Get all Assignment of course. with paging and search
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="sectionId">Course Id</param>
        /// <returns></returns>
        [HttpGet("Get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<AssignmentDTO>>> GetAll([FromQuery] Guid sectionId, [FromQuery] AssignmentParameters assignmentParameters)
        {
            var result = await _assignmentService.GetAll(sectionId, assignmentParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<AssignmentDTO>(true, result));
        }

        //[HttpPost]
        //public async Task<ActionResult<Response<AssignmentDTO>>> Add(AssignmentForCreateRequest assignmentForCreateRequest)
        //{
        //    var result = await _assignmentService.Add(assignmentForCreateRequest);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
