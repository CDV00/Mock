using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _coursesService;
        public CourseController(ICourseService coursesService)
        {
            _coursesService = coursesService;
        }
        [HttpGet]
        public async Task<ActionResult<Responses<CoursesResponse>>> GetAll()
        {
            var result = await _coursesService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Add([FromBody]CourseRequest courseRequest)
        {
            var result = await _coursesService.Add(courseRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
