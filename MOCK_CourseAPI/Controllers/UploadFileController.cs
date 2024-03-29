﻿using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost("UploadFile")]
        //public async Task<ActionResult<UploadResponse>> UploadFile(IFormFile file)
        //{
        //    var result = await _uploadFileService.UploadFile(file);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Download file from Google drive
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="fileId"></param>
        /// <returns>file</returns>
        [HttpGet("DownloadFile/{fileId}")]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            var result = await _uploadFileService.DownloadFileGoogleDrive(fileId);
            if (result.IsSuccess == false)
                return BadRequest(result);

            return File(result.fileData, "application/octet-stream", result.Name);
        }

        /// <summary>
        /// Upload File, file lesser 5mb
        /// </summary>
        /// <remarks>
        /// Use api DownloadFile to download file you already upload
        /// </remarks>
        /// <param name="file"></param>
        /// <returns>id file</returns>
        [HttpPost("Upload-File-Google-Drive")]
        public async Task<ActionResult<UploadResponse>> UploadGoogleDrive(IFormFile file)
        {
            var result = await _uploadFileService.UploadGoogleDrive(file);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
