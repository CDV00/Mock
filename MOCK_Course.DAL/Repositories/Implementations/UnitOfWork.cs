using System;
using System.Threading.Tasks;
using Course.DAL.Data;

namespace Course.DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            //Posts = postRepository;
            //Categories = categoryRepository;
        }

        #region IUnitOfWork Members
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _context.Dispose();
        }

        /// <summary>
        /// Save all changes async
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
