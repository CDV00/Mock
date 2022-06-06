using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ISectionRepositoty : IRepository<Section, Guid>
    {
        ISectionQuery BuildQuery();
        //Task<IEnumerable<Section>> GetAllByCourseId(Guid courseId);
        //Task<bool> RemoveByCourseId(Guid courseId);
    }
}
