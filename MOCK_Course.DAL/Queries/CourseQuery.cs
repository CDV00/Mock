using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class CourseQuery : QueryBase<Courses>, ICourseQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public CourseQuery(IQueryable<Courses> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public ICourseQuery FilterByOrderd(Guid userId)
        {
            var orderIds = _dbContext.Orders.Where(o => o.UserId == userId).Select(o => o.CourseId);

            Query = Query.Where(type => orderIds.Contains(type.Id));
            return this;
        }

        /// <summary>
        /// FilterByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ICourseQuery FilterByUserId(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId);
            return this;
        }

        /// <summary>
        /// FilterIsActive
        /// </summary>
        /// <param name="isActice"></param>
        /// <returns></returns>
        public ICourseQuery FilterIsActive(bool? isActice)
        {
            Query = Query.Where(type => isActice == null || type.IsActive == isActice);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ICourseQuery FilterById(Guid Id)
        {

            Query = Query.Where(type => type.Id == Id);
            return this;
        }

        public ICourseQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }

        public ICourseQuery IncludeLanguage()
        {
            Query.Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).Load();
            return this;
        }

        public ICourseQuery IncludeCategory()
        {
            Query.Include(c => c.Category).Load();
            return this;
        }

        public ICourseQuery IncludeSection()
        {
            Query.Include(c => c.Sections).ThenInclude(s => s.Lectures).Load();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Courses>> GetAllByUserIdAsync(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId
                                        && type.IsActive == true);
            return await Query.OrderByDescending(x => x.CreatedAt).ToListAsync().ConfigureAwait(false);
        }
    }
}
