using Course.BLL.DTO;
using Course.BLL.Services;
using Course.BLL.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
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
    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFileService _uploadFileService;
        public UploadFileController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }


        [HttpPost("UploadImage")]
        public async Task<ActionResult<UploadResponse>> UploadImage(IFormFile image)
        {
            var result = await _uploadFileService.UploadImage(image);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("UploadVideo"), DisableRequestSizeLimit]
        public async Task<ActionResult<UploadResponse>> UploadVideo(IFormFile video)
        {
            var result = await _uploadFileService.UploadVideo(video);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult<UploadResponse>> UploadFile(IFormFile file)
        {
            var result = await _uploadFileService.UploadFile(file);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var result = await _uploadFileService.DownloadFileGoogleDrive(fileName);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return File(result.fileData, "application/octet-stream", fileName);
        }

        [HttpPost("UploadGoogle")]
        public async Task<ActionResult<UploadResponse>> UploadGoogleDrive(IFormFile file)
        {
            var result = await _uploadFileService.UploadGoogleDrive(file);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
