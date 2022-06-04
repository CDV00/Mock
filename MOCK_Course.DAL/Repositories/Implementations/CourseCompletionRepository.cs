using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseCompletionRepository : Repository<CourseCompletion, Guid>, ICourseCompletionRepository
    {
        private AppDbContext _context;
        public CourseCompletionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
