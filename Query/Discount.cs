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
            //Query.Include(c => c.Course).Load();
            //var course = Query.Where(type => type.Courses.UserId == UserId).ToList();

            Query = Query.Where(type => type.Course.UserId == UserId);
            return this;
        }

        public IDiscountQuery FilterByCourseId(Guid courseId)
        {
            //Query.Include(c => c.Course).Load();
            //var course = Query.Where(type => type.Courses.UserId == UserId).ToList();

            Query = Query.Where(type => type.CourseId == courseId);
            return this;
        }

        public IDiscountQuery IncludeOrderItem()
        {
            Query.Include(d => d.OrderItems).Load();
            return this;
        }

        public IDiscountQuery checkDate(Discount discount)
        {
            Query = Query.Where(d => (d.StartDate <= discount.StartDate && d.EndDate >= discount.StartDate) || (d.StartDate <= discount.EndDate && d.EndDate >= discount.EndDate));
            return this;
        }

        public IDiscountQuery CheckDuringDate()
        {
            Query = Query.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now);
            return this;
        }
    }
}
