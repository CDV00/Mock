using Course.BLL.Responses;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICousesRepository : IRepository<Courses, Guid>
    {
        //Task<IEnumerable<Courses>> GetAllForCard();
        //Task<Courses> GetForPost(Guid id);
        //Task<int> GetTotal(Guid userId);
        ////Task<List<MyCoursesDTO>> GetAllMyCoures(Guid id);
        //Task<List<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid id);
        //Task<List<PurchaseDTO>> GetAllMyPurchase(Guid id);
        //Task<List<CousrsePagingDTO>> GetPagingCourses(int page, int pageSize, string sortType);
        //Task<List<CousrsePagingDTO>> GetByFilteringCousrse(string key);
        //Task<CourseForDetailDTO> GetDetail(Guid id);
        //Task<Courses> GetForUpdate(Guid id);
        ICourseQuery BuildQuery();
        //Task<bool> IsFree(Guid courseId);
    }
}
