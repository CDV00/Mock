using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IRepository<T, K> where T : BaseEntity <K>
    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get All Async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        T GetById(K Id);

        /// <summary>
        /// Get by Id Async
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(K Id);

        /// <summary>
        /// Create new
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        void Create(T _object);

        /// <summary>
        /// Create Async
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        Task CreateAsync(T _object);

        /// <summary>
        /// update record
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public bool Update(T _object);

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="_object"></param>
        bool Update(K id,object _object);


        /// <summary>
        /// Remove by Id
        /// </summary>
        /// <param name="_object"></param>
         void Remove(T _object);
    }
}
