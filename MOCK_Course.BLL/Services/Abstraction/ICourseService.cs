using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseService
    {
        Task<ApiBaseResponse> Add(Guid userId, CourseForCreateRequest courseRequest);
        Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest, Guid userId);
        Task<BaseResponse> Remove(Guid id, Guid userId);
        Task<Response<int>> GetTotalCourseOfUser(Guid userId);
        Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId);
        //Task<Responses<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid id);
        Task<Responses<CourseDTO>> GetAllMyPurchase(Guid userId);
        //Task<Responses<CourseDTO>> GetCoursesAsync(CourseParameters courseParameter);
        Task<ApiBaseResponse> GetAllCourses(CourseParameters courseParameter, Guid? userId);
        Task<BaseResponse> UpdateStatus(CourseStatusUpdateRequest courseStatusUpdateRequest);
        Task<Responses<CourseDTO>> UpcomingCourse(Guid userId);
        Task<ApiBaseResponse> GetDetail(Guid id, Guid? userId);
    }
}
