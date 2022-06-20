using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICourseRepository : IRepository<Courses>
    {
        ICourseQuery BuildQuery();
        Task<PagedList<CourseDTO>> GetAllCourseAsync(CourseParameters parameters);
        Task<CourseDTO> GetDetailCourseAsync(Guid id);
        Task<bool> IsExist(Guid id);
    }
}
