using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        //IPostRepository Posts { get; }
        //ICategoryRepository Categories { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
