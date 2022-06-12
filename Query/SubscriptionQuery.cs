using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class EnrollmentQuery : QueryBase<Enrollment>, IEnrollmentQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public EnrollmentQuery(IQueryable<Enrollment> enrollQuery, AppDbContext dbContext) : base(enrollQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public IEnrollmentQuery FilterByCourseId(Guid? CourseId)
        {
            if (CourseId == null)
                return this;
            Query = Query.Where(type => type.CourseId == CourseId);
            return this;
        }

        public IEnrollmentQuery FilterByUserId(Guid? UserId)
        {
            if (UserId == null)
                return this;

            Query = Query.Where(type => type.UserId == UserId);
            return this;
        }

        public IEnrollmentQuery FilterByCourseUserId(Guid? UserId)
        {
            if (UserId == null)
                return this;

            Query = Query.Where(type => type.Courses.UserId == UserId);
            return this;
        }


        public IEnrollmentQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }

        public IEnrollmentQuery IncludeCourse()
        {
            Query.Include(c => c.Courses).Load();
            return this;
        }
    }
}
