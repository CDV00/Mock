using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(Lesson _object)
        {
            _object.IsDeleted = true;
        }
    }
}
