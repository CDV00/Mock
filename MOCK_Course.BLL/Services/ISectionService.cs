using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ISectionService
    {
        Task<Responses<SectionDTO>> GetAll(Guid courseId);
        Task<Response<SectionDTO>> Add(SectionCreateRequest sectionRequest);
        Task<BaseResponse> Remove(Guid idSection);
        Task<Response<SectionDTO>> Update(Guid id, SectionUpdateRequest sectionRequest);
    }
}
