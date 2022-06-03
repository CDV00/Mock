using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Course.BLL.Responses;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CloseCaptionController : ControllerBase
    {
        private readonly ICloseCaptionService _closeCaptionService;
        public CloseCaptionController(ICloseCaptionService closeCaptionService)
        {
            _closeCaptionService = closeCaptionService;
        }
        /// <summary>
        /// Get All CloseCaption and sub-closeCaption
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CloseCaptionDTO>>> GetAll()
        {
            var result = await _closeCaptionService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
