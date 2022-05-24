using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ICourseCompletionService
    {
        Task<Responses<CourseCompletionResponse>> GetAll(Guid userId);
        Task<Response<BaseResponse>> Add(CourseCompletionRequest courseCompletionRequest);
        Task<BaseResponse> Remove(Guid userId);
        Task<Response<CourseCompletionResponse>> Update(CourseCompletionUpdateRequest courseCompletionUpdateRequest);
    }
}
