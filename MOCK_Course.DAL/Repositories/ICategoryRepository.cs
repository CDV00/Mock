using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
