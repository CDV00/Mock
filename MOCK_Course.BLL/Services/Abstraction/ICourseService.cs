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
        Task<Responses<CourseDTO>> GetAll();
        Task<Response<CourseDTO>> Get(Guid id);
        Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest);
        Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest);
        Task<BaseResponse> Remove(Guid id);
        Task<Response<int>> GetTotal(Guid userId);
        Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId);
        //Task<Responses<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid id);
        Task<Responses<CourseDTO>> GetAllMyPurchase(Guid userId);
        //Task<Responses<CourseDTO>> GetCoursesAsync(CourseParameters courseParameter);
        Task<(IList<CourseDTO> courses, MetaData metaData)> GetCoursesAsync(CourseParameters courseParameter);
        //Task<Responses<CousrsePagingDTO>> GetCoursePaing(CousrsePagingRequest cousrsePagingRequest);
        //Task<Responses<CousrsePagingDTO>> GetByFilteringCousrse(string key);
        //Task<Response<CourseForDetailDTO>> GetDetail(Guid id);
    }
}
