using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        /// <summary>
        /// Get all course section by course Id 
        /// https://gambolthemes.net/html-items/cursus_main_demo/create_new_course.html
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Responses<SectionResponse>>> GetAll(Guid id)
        {
            var result = await _sectionService.GetAll(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Create new course section
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="sectionRequest"></param>
        /// <returns></returns>
        [HttpPost("{courseId:guid}")]
        public async Task<ActionResult<Response<SectionResponse>>> Create(Guid courseId,[FromBody]SectionCreateRequest sectionRequest)
        {
            var result = await _sectionService.Add(courseId, sectionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Response<SectionResponse>>> Update(Guid id, [FromBody]SectionUpdateRequest sectionUpdateRequest)
        {
            var result = await _sectionService.Update(id, sectionUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Remove course section
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<BaseResponse>> Remove(Guid id)
        {
            var result = await _sectionService.Remove(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
