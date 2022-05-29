using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICourseCompletionRepository : IRepository<CourseCompletion, Guid>
    {
        Task<bool> IsCompletion(CourseCompletion courseCompletionRequest);
    }
}
