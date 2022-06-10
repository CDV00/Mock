using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;

namespace Repository.Repositories
{
    public class CourseReviewRepository : Repository<CourseReview>, ICourseReviewRepository
    {
        private readonly AppDbContext _context;
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
