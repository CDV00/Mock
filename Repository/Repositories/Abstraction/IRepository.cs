using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid Id);
        Task CreateAsync(T _object);
        Task CreateRangeAsync(List<T> _object);
        bool Update(T _object);
        /// <summary>
        /// remove entity
        /// </summary>
        /// <param name="_object"></param>
        /// <param name="permanent">if permanent == false, it will Set isDeleted = false, else it will delelete permanent</param>
        void Remove(object _object, bool? permanent);
        void RemoveRange(List<T> _object);
        bool UpdateRange(List<T> _objects);
        void ChangeDetachedState(T _object);
    }
}
