using System.Threading.Tasks;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Course.BLL.Responses;
using Course.BLL.Services.Abstraction;

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
        /// Get All CloseCaption 
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
