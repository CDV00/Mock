using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ISectionRepositoty : IRepository<Section, Guid>
    {
        ISectionQuery BuildQuery();
        Task<IEnumerable<Section>> GetAllByCourseId(Guid courseId);
        //Task<bool> RemoveByCourseId(Guid courseId);
    }
}
