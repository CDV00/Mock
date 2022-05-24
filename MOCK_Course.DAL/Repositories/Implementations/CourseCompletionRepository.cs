using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class CourseCompletionRepository : Repository<CourseCompletion>, ICourseCompletionRepository
    {
        public CourseCompletionRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(CourseCompletion _object)
        {
            _object.IsDeleted = true;
        }
    }
}
