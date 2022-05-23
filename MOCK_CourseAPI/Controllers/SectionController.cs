using Course.BLL.Requests;
using Course.BLL.Responses;
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

        [HttpGet]
        public async Task<ActionResult<Responses<SectionResponse>>> GetAll([FromForm]Guid courseId)
        {
            var result = await _sectionService.GetAll(courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<SectionResponse>>> Create([FromForm]SectionRequest sectionRequest)
        {
            var result = await _sectionService.Add(sectionRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Response<SectionResponse>>> Update([FromForm]SectionUpdateRequest sectionUpdateRequest)
        {
            var result = await _sectionService.Update(sectionUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Remove([FromForm] Guid id)
        {
            var result = await _sectionService.Remove(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
