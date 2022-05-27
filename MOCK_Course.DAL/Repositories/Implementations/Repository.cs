using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Course.DAL.Repositories.Implementations
{
    public class Repository<T, K> : IRepository<T, K> where T : BaseEntity<K>
    {
        protected DbSet<T> DbSet;
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            DbSet = context.Set<T>();
            _context = context;
        }

        /// <summary>
        /// Create async
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public async Task CreateAsync(T _object) => await DbSet.AddAsync(_object);

        /// <summary>
        /// Implement Remove method
        /// </summary>
        /// <param name="_object"></param>
        public virtual void Remove(T _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            _object.IsDeleted = true;
        }


        /// <summary>
        /// Implement GetAll method
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll() => DbSet.AsNoTracking();

        /// <summary>
        /// Get by Id async
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>T</returns>
        public async Task<T> GetByIdAsync(K Id)
        {
            var data = await DbSet.FindAsync(Id);
            if (data == null) return null;
            _context.Entry(data).State = EntityState.Modified;
            return data;
        }

        public bool Update(T _object)
        {
            DbSet.Attach(_object);
            _context.Entry(_object).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// Find by Condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => DbSet.Where(expression).AsNoTracking();

        /// <summary>
        /// Count all records
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync() => await DbSet.CountAsync().ConfigureAwait(false);
    }
}
