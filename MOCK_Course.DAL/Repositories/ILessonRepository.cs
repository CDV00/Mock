using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ILectureRepository : IRepository<Lecture, Guid>
    {
        Task<IEnumerable<Lecture>> GetAllBySectionId(Guid sectionId);
        Task<bool> RemoveBySectionId(Guid sectionId);
    }
}
