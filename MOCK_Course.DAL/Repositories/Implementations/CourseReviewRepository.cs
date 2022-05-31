using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseReviewRepository : Repository<CourseReview, Guid>, ICourseReviewRepository
    {
        private AppDbContext _context;
        public CourseReviewRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<int> GetTotal(Guid userId)
        {
            return await GetAll().Where(s => s.Enrollment.UserId == userId).GroupBy(s => s.Enrollment.UserId).CountAsync();
        }

    }
}
