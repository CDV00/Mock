using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.Responsesnamespace;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _LanguageService;
        public LanguageController(ILanguageService LanguageService)
        {
            _LanguageService = LanguageService;
        }
        /// <summary>
        /// Get All Language and sub-Language to create course
        /// https://gambolthemes.net/html-items/cursus_main_demo/create_new_course.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<LanguageResponse>>> GetAll()
        {
            var result = await _LanguageService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
