using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries;
using Entities.ParameterRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICourseRepository : IRepository<Courses>
    {
        ICourseQuery BuildQuery();
        Task<PagedList<CourseDTO>> GetAllCourseAsync(CourseParameters parameters, Guid? userId);
        Task<PagedList<CourseDTO>> GetAllMyCoures(Guid? userId, CourseParameters parameters);
        Task<PagedList<CourseDTO>> UpcomingCourse(Guid? userId, CourseParameters parameters);
        Task<PagedList<CourseDTO>> GetAllMyPurchase(Guid userId, CourseParameters parameters);
        Task<CourseDTO> GetDetailCourseAsync(Guid id);
        Task<bool> IsExist(Guid id);
        Task<PagedList<CourseDTO>> GetAllMyLearning(Guid userId, CourseParameters parameters);
    }
}
