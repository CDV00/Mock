using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ICourseCompletionRepository : IRepository<CourseCompletion, Guid>
    {
    }
}
