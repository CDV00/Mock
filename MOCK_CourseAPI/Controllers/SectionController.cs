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
        public async Task<Responses<SectionResponse>> GetAll(Guid userCourse)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<Response<SectionResponse>> Create(SectionRequest sectionRequest)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<Response<SectionResponse>> Update(SectionUpdateRequest sectionUpdateRequest)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<BaseResponse> Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
