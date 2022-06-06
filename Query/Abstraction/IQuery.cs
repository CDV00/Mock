using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Course.DAL.Queries.Abstraction
{
    /// <summary>
    /// Interface query
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IQuery<TEntity> where TEntity : class
    {
        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        IQuery<TEntity> Take(int? take = null);

        /// <summary>
        /// Take with model
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        IAsyncEnumerable<TOutput> AsAsyncEnumerable<TOutput>(Expression<Func<TEntity, TOutput>> selector);

        /// <summary>
        /// To list async
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<List<TOutput>> ToListAsync<TOutput>(Expression<Func<TEntity, TOutput>> selector);

        /// <summary>
        ///  Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        IQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        ///  Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        IQuery<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        IQuery<TEntity> Skip(int? skip = null);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<long> SumAsync(Expression<Func<TEntity, long>> expression);

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        Task<TOutput> AsSelectorAsync<TOutput>(Expression<Func<TEntity, TOutput>> selector);
    }
}
