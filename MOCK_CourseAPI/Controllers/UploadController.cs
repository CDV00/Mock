using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Instructor")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost("UploadImage")]
        //[AllowedExtensions(new string[] { ".jpg", ".png" })]
        public async Task<ActionResult<UploadImageDTO>> UploadImage(IFormFile image)
        {
            var result = await _uploadService.UploadImage(image);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost("UploadVideo"), DisableRequestSizeLimit]
        public async Task<ActionResult<UploadVideoDTO>> UploadVideo(IFormFile video)
        {
            var result = await _uploadService.UploadVideo(video);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
