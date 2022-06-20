using Course.BLL.DTO;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ISavedCoursesRepository : IRepository<SavedCourses>
    {
        ISavedCoursesQuery BuildQuery();
        Task<PagedList<SavedCoursesDTO>> GetAllSavedCourses(Guid userId, SavedCoursesParameters savedCoursesParameters);
    }
}
