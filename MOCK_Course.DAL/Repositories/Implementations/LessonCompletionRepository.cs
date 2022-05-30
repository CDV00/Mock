using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class LessonCompletionRepository : Repository<LessonCompletion, Guid>, ILessonCompletionRepository
    {
        public LessonCompletionRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> IsCompletion(LessonCompletion lessonCompletion)
        {
            if (await FindByCondition(l => l.UserId == lessonCompletion.UserId && l.LessonId == lessonCompletion.LessonId).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
    }
}
