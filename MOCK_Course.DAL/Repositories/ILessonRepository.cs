using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ILessonRepository : IRepository<Lesson, Guid>
    {
        Task<IEnumerable<Lesson>> GetAllBySectionId(Guid sectionId);
        Task<bool> RemoveBySectionId(Guid sectionId);
    }
}
