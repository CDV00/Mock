using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System;
using Entities.DTOs;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentCompletionController : ControllerBase
    {
        private readonly IAssignmentCompletionService _assignmentCompletionService;
        public AssignmentCompletionController(IAssignmentCompletionService assignmentCompletionService)
        {
            _assignmentCompletionService = assignmentCompletionService;
        }


        /// <summary>
        /// Create new Assignment completion
        ///</summary>
        /// <param name="assignmentCompletionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AssignmentCompletionDTO>> Create([FromBody] AssignmentCompletionRequest assignmentCompletionRequest)
        {
            var userId = User.GetUserId();
            var result = await _assignmentCompletionService.Add(userId, assignmentCompletionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// check completion lecture
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpGet("Is-Completion")]
        public async Task<ActionResult<BaseResponse>> IsCompletion(Guid assignmentId)
        {
            var userId = User.GetUserId();
            var result = await _assignmentCompletionService.IsCompletion(userId, assignmentId);
            if (result.IsSuccess == false && result.StatusCode != null)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
