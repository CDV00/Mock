using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface ILessonCompletionService
    {
        Task<BaseResponse> IsSucceed(LessonCompletionRequest lessonCompletionRequest);
        Task<BaseResponse> Add(LessonCompletionRequest lessonCompletionRequest);
        //Task<BaseResponse> Remove(Guid userId);
        //Task<Response<LessonCompletionResponse>> Update(LessonCompletionUpdateRequest lessonCompletionUpdateRequest);
    }
}
