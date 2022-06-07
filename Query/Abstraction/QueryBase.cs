using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SES.HomeServices.Data.Queries.Abstractions
{
    /// <summary>
    /// IQuery abstract class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class
    {
        /// <summary>
        /// _query
        /// </summary>
        protected IQueryable<TEntity> Query { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="query"></param>
        protected QueryBase(IQueryable<TEntity> query)
        {
            Query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public virtual async Task<TOutput> AsSelectorAsync<TOutput>(Expression<Func<TEntity, TOutput>> selector)
        {
            return await Query.Select(selector).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Take with model
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual IAsyncEnumerable<TOutput> AsAsyncEnumerable<TOutput>(Expression<Func<TEntity, TOutput>> selector)
        {
            return Query
            .Select(selector)
            .AsAsyncEnumerable();
        }

        /// <summary>
        /// Take with model
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task<List<TOutput>> ToListAsync<TOutput>(Expression<Func<TEntity, TOutput>> selector)
        {
            return await Query
                .Select(selector)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Where
        /// </summary>
        public virtual async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///  Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IQuery<TEntity> Skip(int? skip = null)
        {
            if (skip.HasValue)
            {
                Query = Query.Skip(skip.Value);
            }

            return this;
        }

        /// <summary>
        ///  Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            Query = Query.OrderBy(keySelector);

            return this;
        }

        /// <summary>
        ///  Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IQuery<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            Query = Query.OrderByDescending(keySelector);

            return this;
        }

        /// <summary>
        ///  Take
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        public IQuery<TEntity> Take(int? take = null)
        {
            if (take.HasValue)
            {
                Query = Query.Take(take.Value);
            }

            return this;
        }

        /// <summary>
        /// Any async
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AnyAsync()
        {
            return await Query.AnyAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<long> SumAsync(Expression<Func<TEntity, long>> expression)
        {
            return await Query.SumAsync(expression).ConfigureAwait(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await Query.CountAsync().ConfigureAwait(false);
        }


        public IQuery<TEntity> ApplySort(string orderby)
        {
            if (string.IsNullOrWhiteSpace(orderby))
            {
                return this;
            }

            var orderParams = orderby.Trim().Split(',');
            var propertyInfos = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrEmpty(orderQuery))
            {
                return this;
            }
            return this;
        }
    }
}
