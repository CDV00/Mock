using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseCompletionRepository : Repository<CourseCompletion, Guid>, ICourseCompletionRepository
    {
        public CourseCompletionRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> IsCompletion(CourseCompletion courseCompletion)
        {
            if (await FindByCondition(l => l.CourseId == courseCompletion.CourseId && l.UserId == courseCompletion.UserId).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
    }
}
