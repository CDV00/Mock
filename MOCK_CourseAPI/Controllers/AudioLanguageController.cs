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
    public class AudioLanguageController : ControllerBase
    {
        private readonly IAudioLanguageService _audioLanguageService;
        public AudioLanguageController(IAudioLanguageService audioLanguageService)
        {
            _audioLanguageService = audioLanguageService;
        }
        /// <summary>
        /// Get All AudioLanguage and sub-audioLanguage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<AudioLanguageDTO>>> GetAll()
        {
            var result = await _audioLanguageService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return BadRequest(result);
            return Ok(result);
        }
    }
}
