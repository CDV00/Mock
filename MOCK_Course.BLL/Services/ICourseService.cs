using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface ICourseService
    {
        Task<Responses<CoursesCardResponse>> GetAll();
        Task<Response<CourseResponse>> Get(Guid id);
        Task<Response<CourseResponse>> Add(CourseRequest courseRequest);
        Task<Response<CourseResponse>> Update(Guid id, UpdateCourseRequest courseRequest);
        Task<BaseResponse> Remove(Guid id);
    }
}
