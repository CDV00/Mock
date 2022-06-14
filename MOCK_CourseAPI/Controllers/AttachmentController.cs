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
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        /// <summary>
        /// Get all review of course. with paging and search
        /// https://gambolthemes.net/html-items/cursus_main_demo/course_detail_view.html
        /// </summary>
        /// <param name="CourseId">Course Id</param>
        /// <returns></returns>
        [HttpGet("Get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<AttachmentDTO>>> GetAll()
        {
            var result = await _attachmentService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<AttachmentDTO>>> Add([FromForm]AttachmentForCreateRequest attachmentForCreateRequest)
        {
            var result = await _attachmentService.Add(attachmentForCreateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }




    }
}
