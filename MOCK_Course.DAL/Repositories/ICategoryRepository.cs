using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        ICategoryQuery BuildQuery();
    }
}
