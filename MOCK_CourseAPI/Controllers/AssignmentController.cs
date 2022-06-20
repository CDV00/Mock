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
using Course.DAL.DTOs;
using Entities.Responses;
using Entities.Extension;
using CourseAPI.Presentation.Controllers;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentController : ApiControllerBase
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
        /// <returns></returns>
        [HttpGet("Get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponses<AssignmentDTO>>> GetAllAssignment([FromQuery] AssignmentParameters parameter)
        {
            var result = await _assignmentService.GetAllAssignment(parameter);
            if (!result.IsSuccess)
                return ProcessError(result);
            var coursePagedList = result.GetResult<PagedList<AssignmentDTO>>();

            Response.Headers.Add("X-Pagination",
                              JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(result);
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
