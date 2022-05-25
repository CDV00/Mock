using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class LessonCompletionRepository : Repository<LessonCompletion, Guid>, ILessonCompletionRepository
    {
        public LessonCompletionRepository(AppDbContext context): base(context)
        {

        }
    }
}
