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
        Task<int> TotalLectureCompletionBycourse(Guid userId, Guid courseId);
        Task<int> TotalLectureCompletionBySection(Guid userId, Guid sectionId);
    }
}
