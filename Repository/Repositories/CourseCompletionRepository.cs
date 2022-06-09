using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseCompletionRepository : Repository<CourseCompletion>, ICourseCompletionRepository
    {
        private AppDbContext _context;
        public CourseCompletionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
