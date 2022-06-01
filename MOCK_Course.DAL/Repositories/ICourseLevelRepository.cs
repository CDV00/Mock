using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICourseLevelRepository : IRepository<CourseLevel, Guid>
    {
        Task<bool> RemoveAll(Guid courseId);
    }
}
