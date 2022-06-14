using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;
using System;

namespace Course.BLL.Services.Abstraction
{
    public interface ILectureCompletionService
    {
        Task<BaseResponse> Add(System.Guid userId, LectureCompletionRequest lessonCompletionRequest);
        Task<BaseResponse> IsCompletion(Guid userId, Guid lectureId);
        Task<int> totalLectureCompletionBycourse(Guid userId, Guid courseId);

        //Task<BaseResponse> Remove(Guid userId);
        //Task<Response<LessonCompletionResponse>> Update(LessonCompletionUpdateRequest lessonCompletionUpdateRequest);
        Task<int> totalLectureCompletionBySection(Guid userId, Guid sectionId);
    }
}
