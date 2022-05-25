using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class LessonCompletionRepository : Repository<LessonCompletion>, ILessonCompletionRepository
    {
        public LessonCompletionRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(LessonCompletion _object)
        {
            _object.IsDeleted = true;
        }
    }
}
