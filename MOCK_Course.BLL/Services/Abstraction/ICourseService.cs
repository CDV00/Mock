using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.DAL.DTOs;
using Course.BLL.Share.RequestFeatures;
using System.Collections.Generic;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseService
    {
        Task<Response<CourseDTO>> GetAll();
        Task<Response<CourseDTO>> Get(Guid id);
        Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest);
        Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest, Guid userId);
        Task<BaseResponse> Remove(Guid id, Guid userId);
        Task<Response<int>> GetTotalCourseOfUser(Guid userId);
        Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId);
        //Task<Responses<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid id);
        Task<Responses<CourseDTO>> GetAllMyPurchase(Guid userId);
        //Task<Responses<CourseDTO>> GetCoursesAsync(CourseParameters courseParameter);
        Task<PagedList<CourseDTO>> GetCoursesAsync(CourseParameters courseParameter);
    }
}
