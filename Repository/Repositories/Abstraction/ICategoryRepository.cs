using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICategoryRepository : IRepository<Category>
    {
        ICategoryQuery BuildQuery();
        Task<bool> Existing(Guid? id);
    }
}
