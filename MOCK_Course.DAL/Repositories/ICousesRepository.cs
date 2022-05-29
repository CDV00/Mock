using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICousesRepository : IRepository<Courses, Guid>
    {
        Task<IEnumerable<Courses>> GetAllForCard();
        Task<Courses> GetForPost(Guid id);
    }
}
