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
        /// Implement Create method
        /// </summary>
        /// <param name="_object"></param>
        public void Create(T _object) => DbSet.Add(_object);

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
        /// Implement GetAllAsync method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await DbSet.AsNoTracking().ToListAsync();

        /// <summary>
        /// Implement GetId method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T GetById(K Id)
        {
            var data = DbSet.Find(Id);
            if (data == null) return null;
            _context.Entry(data).State = EntityState.Detached;
            return data;
        }

        /// <summary>
        /// Get by Id async
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>T</returns>
        public async Task<T> GetByIdAsync(K Id)
        {
            var data = await DbSet.FindAsync(Id);
            if (data == null) return null;
            _context.Entry(data).State = EntityState.Detached;
            return data;
        }

        /// <summary>
        /// Implement Update method
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public bool Update(K id, object _object)
        {
            //DbSet.Attach(_object);
            //_context.Entry(_object).State = EntityState.Modified;
            //return true;
            var entity = DbSet.Find(id);
            if (entity == null)
            {
                return false;
            }

            _context.Entry(entity).CurrentValues.SetValues(_object);
            return true;
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
