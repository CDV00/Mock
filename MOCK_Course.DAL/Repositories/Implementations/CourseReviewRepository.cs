using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseReviewRepository : Repository<CourseReview, Guid>, ICourseReviewRepository
    {
        private AppDbContext _context;
        public CourseReviewRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public ICourseReviewQuery BuildQuery()
        {
            return new CourseReviewQuery(_context.CourseReviews.AsQueryable(), _context);
        }
        //public async Task<int> GetTotal(Guid userId)
        //{
        //    return await GetAll().Where(s => s.Enrollment.UserId == userId).GroupBy(s => s.Enrollment.UserId).CountAsync();
        //}

    }
}
