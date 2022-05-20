using Course.BLL.Requests;
using Course.BLL.Responses;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class SectionService : ISectionService
    {
        public Task<Response<SectionResponse>> Add(SectionRequest sectionRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Responses<SectionResponse>> GetAll(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> Remove(Guid idSection)
        {
            throw new NotImplementedException();
        }

        public Task<Response<SectionResponse>> Update(SectionUpdateRequest sectionRequest)
        {
            throw new NotImplementedException();
        }
    }
}
