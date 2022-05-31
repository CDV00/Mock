using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface ILectureCompletionService
    {
        Task<BaseResponse> Add(System.Guid userId, LectureCompletionRequest lessonCompletionRequest);
        //Task<BaseResponse> Remove(Guid userId);
        //Task<Response<LessonCompletionResponse>> Update(LessonCompletionUpdateRequest lessonCompletionUpdateRequest);
    }
}
