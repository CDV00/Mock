using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseCompletionRepository : Repository<CourseCompletion, Guid>, ICourseCompletionRepository
    {
        public CourseCompletionRepository(AppDbContext context): base(context)
        {

        }
    }
}
