using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ISavedCoursesRepository : IRepository<SavedCourses, Guid>
    {
        ISavedCoursesQuery BuildQuery();
    }
}
