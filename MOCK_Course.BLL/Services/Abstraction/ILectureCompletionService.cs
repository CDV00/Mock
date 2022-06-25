using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ILectureCompletionService
    {
        Task<Response<LectureCompletionDTO>> Add(Guid userId, LectureCompletionRequest lectureCompletionRequest);
        Task<BaseResponse> IsCompletion(Guid userId, Guid lectureId);
        Task<int> TotalLectureCompletionBycourse(Guid userId, Guid courseId);
        Task<int> TotalLectureCompletionBySection(Guid userId, Guid sectionId);
        Task<Response<LectureCompletionDTO>> Update(Guid userId, LectureCompletionRequest lectureCompletionRequest);
    }
}
