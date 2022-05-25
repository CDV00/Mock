using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class LessonRepository : Repository<Lesson, Guid>, ILessonRepository
    {
        public LessonRepository(AppDbContext context): base(context)
        {

        }
    }
}
