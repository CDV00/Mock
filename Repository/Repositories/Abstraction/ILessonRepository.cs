using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ILectureRepository : IRepository<Lecture>
    {
        ILectureQuery BuildQuery();
        Task<bool> RemoveBySectionId(Guid sectionId);
    }
}
