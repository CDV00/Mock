using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ILessonCompletionService
    {
        Task<Responses<LessonCompletionResponse>> GetAll(Guid userId);
        Task<Response<BaseResponse>> Add(LessonCompletionRequest lessonCompletionRequest);
        Task<BaseResponse> Remove(Guid userId);
        Task<Response<LessonCompletionResponse>> Update(LessonCompletionUpdateRequest lessonCompletionUpdateRequest);
    }
}
