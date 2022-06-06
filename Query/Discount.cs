using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;
using Course.DAL.Data;
using Course.DAL.Queries.Abstraction;

namespace Course.Queries
{
    public class DiscountQuery : QueryBase<Discount>, IDiscountQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public DiscountQuery(IQueryable<Discount> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        public IDiscountQuery IncludeCourses()
        {
            Query.Include(c => c.Course).Load();
            return this;
        }
        public IDiscountQuery FilterByUserId(Guid UserId)
        {
            Query.Include(c => c.Course).Load();
            //var course = Query.Where(type => type.Courses.UserId == UserId).ToList();

            Query = Query.Where(type => type.Course.UserId == UserId);
            return this;
        }
    }
}
