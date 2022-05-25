using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ICourseService
    {
        Task<Responses<CoursesCartResponse>> GetAll();
        Task<Response<CourseResponse>> Add(CourseRequest courseRequest);
        Task<Response<CourseResponse>> Update(UpdateCourseRequest courseRequest);
        Task<BaseResponse> Remove(Guid idCourse);
    }
}
