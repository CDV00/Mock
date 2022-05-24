using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;

namespace Course.BLL.Services
{
    public interface ICourseService
    {
        Task<Responses<CoursesResponse>> GetAll();
        Task<Response<CoursesResponse>> Add(CourseRequest courseRequest);
    }
}
