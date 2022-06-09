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
        public bool Update(T _object);
        void Remove(T _object);
        void RemoveRange(List<T> _object);
    }
}
