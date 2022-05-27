using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
