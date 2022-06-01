using Course.DAL.DTOs;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICousesRepository : IRepository<Courses, Guid>
    {
        Task<IEnumerable<Courses>> GetAllForCard();
        Task<Courses> GetForPost(Guid id);
        Task<int> GetTotal(Guid userId);
        Task<List<MyCoursesDTO>> GetAllMyCoures(Guid id);
        Task<List<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid id);
        Task<List<PurchaseDTO>> GetAllMyPurchase(Guid id);
    }
}
