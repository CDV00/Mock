using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ILessonService
    {
        Task<Responses<LessonResponse>> GetAll(Guid courseId);
        Task<Response<LessonResponse>> Add(LessonCreateRequest LessonRequest);
        Task<BaseResponse> Remove(Guid idLesson);
        Task<Response<LessonResponse>> Update(LessonUpdateRequest LessonRequest);
    }
}
