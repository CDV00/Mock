using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ICousesRepository : IRepository<Courses, Guid>
    {
    }
}
