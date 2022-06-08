using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class CourseReviewQuery : QueryBase<CourseReview>, ICourseReviewQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public CourseReviewQuery(IQueryable<CourseReview> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public ICourseReviewQuery FilterByCourseId(Guid CourseId)
        {
            Query.Include(c => c.Enrollment).Load();
            Query = Query.Where(type => type.Enrollment.CourseId == CourseId);
            return this;
        }

        public async Task<float> GetAvgRate()
        {
            //Query.Include(c => c.Enrollment).Load();
            return await Query.AverageAsync(c => c.Rating);
        }

        public async Task<float> GetAvgRatePercent(long sum)
        {
            //Query.Include(c => c.Enrollment).Load();
            //_dbContext.CourseReviews.AsQueryable().Where(c=>c.)
            var result = (await Query.AverageAsync(c => c.Rating) * 100) / sum;
            return result;
        }

        public ICourseReviewQuery FilterByRating(float Rating)
        {
            Query = Query.Where(type => type.Rating == Rating);
            return this;
        }


        public ICourseReviewQuery FilterByUserId(Guid UserId)
        {
            Query.Include(c => c.Enrollment).Load();
            Query = Query.Where(type => type.Enrollment.UserId == UserId);
            return this;
        }

        public ICourseReviewQuery IncludeEnrollment()
        {
            Query.Include(c => c.Enrollment).Load();
            return this;
        }

        public ICourseReviewQuery IncludeUser()
        {
            Query.Include(c => c.Enrollment.User).Load();
            return this;
        }

        public ICourseReviewQuery IncludeCourse()
        {
            Query.Include(c => c.Enrollment.Courses).Load();
            return this;
        }
    }
}
