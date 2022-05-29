using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ILessonService
    {
        Task<Responses<LessonDTO>> GetAll(Guid courseId);
        Task<Response<LessonDTO>> Add(LessonForCreateRequest LessonRequest);
        Task<BaseResponse> Remove(Guid idLesson);
        Task<Response<LessonDTO>> Update(Guid id, LessonForUpdateRequest LessonRequest);
    }
}
