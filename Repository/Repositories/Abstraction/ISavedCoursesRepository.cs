using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ISavedCoursesRepository : IRepository<SavedCourses>
    {
        ISavedCoursesQuery BuildQuery();
        Task<bool> CheckExistSaveCourse(Guid courseId, Guid userId);
        Task<PagedList<SavedCoursesDTO>> GetAllSavedCourses(Guid userId, SavedCoursesParameters savedCoursesParameters);
    }
}
