using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ILessonService
    {
        Task<Responses<LessonResponse>> GetAll(Guid courseId);
        Task<Response<LessonResponse>> Add(Guid SectionId, LessonCreateRequest LessonRequest);
        Task<BaseResponse> Remove(Guid idLesson);
        Task<Response<LessonResponse>> Update(Guid id,LessonUpdateRequest LessonRequest);
    }
}
