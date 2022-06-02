using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.DAL.DTOs;

namespace Course.BLL.Services
{
    public interface ICourseService
    {
        Task<Responses<CoursesCardDTO>> GetAll();
        Task<Response<CourseDTO>> GetForPost(Guid id);
        Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest);
        Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest);
        Task<BaseResponse> Remove(Guid id);
        Task<Response<int>> GetTotal(Guid userId);
        Task<Responses<MyCoursesDTO>> GetAllMyCoures(Guid id);
        Task<Responses<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid id);
        Task<Responses<PurchaseDTO>> GetAllMyPurchase(Guid id);
        Task<Response<CourseForDetailDTO>> GetDetail(Guid id);
    }
}
